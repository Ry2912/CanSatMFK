#include <string.h>

// ピン指定
#define TWE_Serial Serial1
#define GPS_Serial Serial2

#define MOTOR_R_1 12
#define MOTOR_R_2 13
#define MOTOR_L_1 10
#define MOTOR_L_2 11

#define LED_1 9
#define LED_2 8
#define LED PIN_LED0

#define SWITCH 15

#define TEMP_SENSOR 14
#define TWE_1 3
#define TWE_2 4

// 並列処理の間隔
#define LOOP_TEMP 100 // 100 * 0.01 = 1
#define LOOP_GPS 100 // 100 * 0.01 = 1
#define LOOP_TWE 200 // 200 * 0.01 = 2
#define LOOP_AUTO 100 // 100 * 0.01 = 1

// 4つのモード
#define STOP_MODE 0
#define CONTROL_MODE 1
#define AUTO_MODE 2
#define TEST_MODE 3

int mode;

int temp_cnt; // 温度センサカウント
int gps_cnt; // GPSカウント
int twe_cnt; // TweLiteカウント
int auto_cnt; // オートモードカウント

const int TEST = 1; // 0のとき、シリアルモニターでテスト

byte receive; // 受信したデータ
int command; // 受信したモータ制御のコマンド
char buffer[8];
int count = 0;

float temp; // 温度

float now_lat; // 今の緯度
float now_lon; // 今の経度

float before_lat; // 前の緯度
float before_lon; // 前の経度

float dest_lat; // 目標緯度
float dest_lon; // 目標経度

//int now_lat_int;
//int now_lon_int;
//int now_lat_d;
//int now_lon_d;

int lat_dd;
int lon_ddd;
int lat_mm;
int lat_mmmm;
int lon_mm;
int lon_mmmm;

int lat_mmmmmm;
int lon_mmmmmm;

int lat_dddddd;
int lon_dddddd;


float fmap(float x, long in_min, long in_max, long out_min, long out_max) // map()のfloat版
{
    return (float)((x - in_min) * (out_max - out_min) / (in_max - in_min)) + out_min;
}

// ---------------------モータ---------------------------

void R_motor_control(int act)
{
    // 0 : stop
    // 1 : forward
    // 2 : back
    // 3 : brake

    if (act == 0)
    {
        digitalWrite(MOTOR_R_1, LOW);
        digitalWrite(MOTOR_R_2, LOW);
    }
    else if (act == 1)
    {
        digitalWrite(MOTOR_R_1, HIGH);
        digitalWrite(MOTOR_R_2, LOW);
    }
    else if (act == 2)
    {
        digitalWrite(MOTOR_R_1, LOW);
        digitalWrite(MOTOR_R_2, HIGH);
    }
    else if (act == 3)
    {
        digitalWrite(MOTOR_R_1, HIGH);
        digitalWrite(MOTOR_R_2, HIGH);
    }
}

void L_motor_control(int act)
{
    // 0 : stop
    // 1 : forward
    // 2 : back
    // 3 : brake

    if (act == 0)
    {
        digitalWrite(MOTOR_L_1, LOW);
        digitalWrite(MOTOR_L_2, LOW);
    }
    else if (act == 1)
    {
        digitalWrite(MOTOR_L_1, HIGH);
        digitalWrite(MOTOR_L_2, LOW);
    }
    else if (act == 2)
    {
        digitalWrite(MOTOR_L_1, LOW);
        digitalWrite(MOTOR_L_2, HIGH);
    }
    else if (act == 3)
    {
        digitalWrite(MOTOR_L_1, HIGH);
        digitalWrite(MOTOR_L_2, HIGH);
    }
}

void motor_control(int R_act, int L_act)
{
    R_motor_control(R_act);
    L_motor_control(L_act);
}

void run_forward()
{
    motor_control(1, 1);
}

void run_back()
{
    motor_control(2, 2);
}

void turn_right()
{
    motor_control(1, 2);
}

void turn_left()
{
    motor_control(2, 1);
}

void run_stop()
{
    motor_control(0, 0);
}

// -----------------------------------------------------


// ----------------温度センサ---------------------------
float temprature()
{
    int sensor, v;
    float ans;

    sensor = analogRead(TEMP_SENSOR); // センサ値
    v = fmap(sensor, 0, 675, 0, 3300); // センサ値を電圧に変換する
    ans = fmap(v, 600, 3200, -30, 100);

    return ans;
}


// ---------------スイッチ-------------------------------
void swich()
{
    if (digitalRead(SWITCH) == LOW)
    {
        while (digitalRead(SWITCH) == LOW)
        {

        }
        motor_control(0, 0);
        mode++;
        if (mode >= 4)
        {
            mode = 0;
        }
        delay(10);
    }
}

// ---------------LED-----------------------------------
void led_control(int mode)
{
    if (mode == STOP_MODE)
    {
        digitalWrite(LED_1, LOW);
        digitalWrite(LED_2, LOW);
    }
    else if (mode == CONTROL_MODE)
    {
        digitalWrite(LED_1, HIGH);
        digitalWrite(LED_2, LOW);
    }
    else if (mode == AUTO_MODE)
    {
        digitalWrite(LED_1, LOW);
        digitalWrite(LED_2, HIGH);
    }
    else if (mode == TEST_MODE)
    {
        digitalWrite(LED_1, HIGH);
        digitalWrite(LED_2, HIGH);
    }
}



// --------------------自律制御-------------------------
void run_auto(float now_lat, float now_lon, float before_lat, float before_lon, float dest_lat, float dest_lon)
{
    // vec: 進んでいる向き
    float vec_lat = now_lat - before_lat;
    float vec_lon = now_lon - before_lon;

    // dest_vec: 目標への向き
    float dest_vec_lat = dest_lat - now_lat;
    float dest_vec_lon = dest_lon - now_lon;

    float a, b; // 正規化の係数
    a = 1 / (vec_lat * vec_lat + vec_lon * vec_lon);
    b = 1 / (dest_vec_lat * dest_vec_lat + dest_vec_lon * dest_vec_lon);

    float _vec_lat = a * vec_lat;
    float _vec_lon = a * vec_lon;
    float _dest_vec_lat = b * dest_vec_lat;
    float _dest_vec_lon = b * dest_vec_lon;

    // 外積をとったz成分
    float flag = (_vec_lat * _dest_vec_lon) - (_vec_lon * _dest_vec_lat);

    if (flag > 0.5)
    {
        turn_right();
    }
    else if (flag < -0.5)
    {
        turn_left();
    }
    else
    {
        run_forward();
    }
}



void setup()
{
    mode = STOP_MODE;

    pinMode(MOTOR_R_1, OUTPUT);
    pinMode(MOTOR_R_2, OUTPUT);
    pinMode(MOTOR_L_1, OUTPUT);
    pinMode(MOTOR_L_2, OUTPUT);

    pinMode(LED_1, OUTPUT);
    pinMode(LED_2, OUTPUT);
    pinMode(LED, OUTPUT);

    pinMode(SWITCH, INPUT);

    pinMode(TEMP_SENSOR, INPUT);
    pinMode(TWE_1, INPUT);
    pinMode(TWE_2, INPUT);

    temp_cnt = 0;
    gps_cnt = 0;

    now_lat = 0.0;
    now_lon = 0.0;
    
    dest_lat = 34.2319989;
    dest_lon = 135.1887434;

    Serial.begin(115200); // for USB
    GPS_Serial.begin(9600); // for GPS
    TWE_Serial.begin(115200); // for TWE
    delay(2000);

    Serial.println("---------------------------------");
    Serial.println("hello, this is rover robot system");
    Serial.println("---------------------------------");
    Serial.println("start!");
    Serial.println("");


    motor_control(0, 0);
    digitalWrite(LED, LOW);
}


void loop()
{
    swich();
    led_control(mode);

    // ------------------温度読み取り-------------------------

    if (temp_cnt == 0) // 並列処理
    {
        temp = temprature();

        if (TEST == 0)
        {
            Serial.print("temprature ");
            Serial.println(temp, 1);
            Serial.println("");
        }
    }
    temp_cnt++;
    if (temp_cnt >= LOOP_TEMP)
    {
        temp_cnt = 0;
    }

    // -------------------------------------------------------


    // ------------------GPS読み取り--------------------------
    
    if (gps_cnt == 0) // 並列処理
    {
        for (int k = 0; k < 10; k++)
        {
            // 1つのセンテンスを読み込む
            String line = GPS_Serial.readStringUntil('\n');

            if (line != "")
            {
                int i, index = 0;
                int len = line.length();
                String str = "";

                // StringListの生成(簡易)
                String list[30];
                for (i = 0; i < 30; i++)
                {
                    list[i] = "";
                }

                // 「,」を区切り文字として文字列を配列にする
                for (i = 0; i < len; i++)
                {
                    if (line[i] == ',')
                    {
                        list[index++] = str;
                        str = ""; 

                        if (list[0] != "$GPRMC") // $GPRMCでないなら後は見る必要がない
                        {
                            break;
                        }

                        continue;
                    }
                    str += line[i];


                }

                // $GPRMCセンテンスのみ読み込む
                if (list[0] == "$GPRMC")
                {
                    if (list[2] == "A") // A: データ有効、V: データ無効
                    {
                        //int dot_lat = list[3].indexOf('.') + 1;
                        //int dot_lon = list[5].indexOf('.') + 1;

                        //int len_lat = list[3].length();
                        //int len_lon = list[5].length();

                        //now_lat_int = list[3].toInt();
                        //now_lon_int = list[5].toInt();
                        //now_lat_d = (list[3].substring(dot_lat, len_lat)).toInt();
                        //now_lon_d = (list[5].substring(dot_lon, len_lon)).toInt();


                        lat_dd = (list[3].substring(0, 2)).toInt();
                        lon_ddd = (list[5].substring(0, 3)).toInt();

                        lat_mm = (list[3].substring(2, 4)).toInt();
                        lat_mmmm = (list[3].substring(5, 9)).toInt();
                        lon_mm = (list[5].substring(3, 5)).toInt();
                        lon_mmmm = (list[5].substring(6, 10)).toInt();

                        
                        lat_mmmmmm = lat_mm * 10000 + lat_mmmm;
                        lon_mmmmmm = lon_mm * 10000 + lon_mmmm;

                        lat_dddddd = lat_mmmmmm / 0.6;
                        lon_dddddd = lon_mmmmmm / 0.6;

                        now_lat = ((float)lat_dd) + ((float)lat_mmmmmm / 600000);
                        now_lon = ((float)lon_ddd) + ((float)lon_mmmmmm / 600000);


                        if (TEST == 0)
                        {
                            // 緯度
                            Serial.print("lat: ");
                            Serial.println(now_lat, 6);
                            //Serial.println(list[3]);
                            //Serial.println("");

                            // 経度
                            Serial.print("lon: ");
                            Serial.println(now_lon, 6);
                            //Serial.println(list[5]);
                            Serial.println("");
                        }

                    }
                    else
                    {
                        if (TEST == 0)
                        {
                            Serial.print("error");
                            Serial.println("");
                        }
                    }

                }
            }
        }
    }
    gps_cnt++;
    if (gps_cnt >= LOOP_GPS)
    {
        gps_cnt = 0;
    }

    // ---------------------------------------------------


    //----------RoboからPCに送るstring----------
    // : 02 01 data(HEX) X \r\n
    // ~1 ~2 ~3        ~4~5   ~6
    // 
    // 1 ヘッダ
    // 2 PCアドレス
    // 3 UART設定
    // 4 送りたいデータ(16進)
    // 5 チェックサム(省略するのでX)
    // 6 改行コード

    if (mode != STOP_MODE)
    {
        if (twe_cnt == 0) // 並列処理
        {
            //if (TWE_Serial.available()) // これがあると一生読み込まない…
            {
                digitalWrite(LED, HIGH);

                TWE_Serial.print(":0201");
                TWE_Serial.print("A"); // 緯度のマーク
                TWE_Serial.print(lat_dd); // 緯度送信
                TWE_Serial.print(lat_dddddd); // 緯度送信
                TWE_Serial.print("B"); // 経度のマーク
                TWE_Serial.print(lon_ddd); // 経度送信
                TWE_Serial.print(lon_dddddd); // 経度送信
                TWE_Serial.print("C"); // 温度のマーク
                TWE_Serial.print((int)(temp * 10)); // 温度送信
                TWE_Serial.print("DD"); // データ最後のマーク
                TWE_Serial.print("X\r\n");

                //Serial.println ("000000000000");

                digitalWrite(LED, LOW);
            }
        }
        twe_cnt++;
        if (twe_cnt >= LOOP_TWE)
        {
            twe_cnt = 0;
        }
    }


    // TWE-LITE受信
    while (TWE_Serial.available())
    {
        receive = TWE_Serial.read();
        //Serial.print((char)receive);
        if ((char)receive == ':')
        {
            digitalWrite(LED, HIGH);
            for (count = 0; count < 8; count++)
            {
                buffer[count] = (char)receive;
                receive = TWE_Serial.read();
            }
            if (buffer[3] == '0' && buffer[4] == '1')
            {
                if (TEST == 0)
                {
                    for (count = 0; count < 12; count++)
                    {
                        Serial.print(buffer[count]);
                    }
                    Serial.println("");
                }

                // モードチェンジ関係
                if (buffer[5] == '2' && buffer[6] == '3')
                {
                    mode = STOP_MODE;
                    motor_control(0, 0);
                }
                else if (buffer[5] == '2' && buffer[6] == '5')
                {
                    mode = CONTROL_MODE;
                    motor_control(0, 0);
                }
                else if (buffer[5] == '2' && buffer[6] == '7')
                {
                    mode = AUTO_MODE;
                    motor_control(0, 0);
                }
                else if (buffer[5] == '2' && buffer[6] == '9')
                {
                    mode = TEST_MODE;
                    motor_control(0, 0);
                }
                // モータコントロール関係
                else if (buffer[5] == '0' && buffer[6] == '0' && mode == CONTROL_MODE)
                {
                    delay(5);
                    run_forward();
                }
                else if (buffer[5] == '1' && buffer[6] == '1' && mode == CONTROL_MODE)
                {
                    delay(5);
                    run_back();
                }
                else if (buffer[5] == '9' && buffer[6] == '9' && mode == CONTROL_MODE)
                {
                    delay(5);
                    run_stop();
                }
                else if (buffer[5] == '4' && buffer[6] == '4' && mode == CONTROL_MODE)
                {
                    delay(5);
                    turn_right();
                }
                else if (buffer[5] == '6' && buffer[6] == '6' && mode == CONTROL_MODE)
                {
                    delay(5);
                    turn_left();
                }

                else if (buffer[5] == '3' && buffer[6] == '1' && mode == CONTROL_MODE)
                {
                    delay(5);
                    // 右だけ前
                    motor_control(1, 0);
                }
                else if (buffer[5] == '3' && buffer[6] == '3' && mode == CONTROL_MODE)
                {
                    delay(5);
                    // 右だけ後
                    motor_control(2, 0);
                }
                else if (buffer[5] == '5' && buffer[6] == '1' && mode == CONTROL_MODE)
                {
                    delay(5);
                    // 左だけ前
                    motor_control(0, 1);
                }
                else if (buffer[5] == '5' && buffer[6] == '3' && mode == CONTROL_MODE)
                {
                    delay(5);
                    // 左だけ後
                    motor_control(0, 2);
                }
            }
        }
        digitalWrite(LED, LOW);
        // Serial.print(char(receive));
    }



    switch (mode)
    {
    case STOP_MODE:

        break;

    case CONTROL_MODE:
        

        break;

    case AUTO_MODE:

        if (auto_cnt == 0)
        {
            run_auto(now_lat, now_lon, before_lat, before_lon, dest_lat, dest_lon);

            before_lat = now_lat;
            before_lon = now_lon;
        }
        auto_cnt++;
        if (auto_cnt >= LOOP_AUTO)
        {
            auto_cnt = 0;
        }

        break;

    case TEST_MODE:

        run_forward();

        break;
    }

    delay(10);
}


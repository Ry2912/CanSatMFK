#include<string.h>

#define TWE_Serial Serial2
#define LED PIN_LED0

#define DATA_SIZE 10

String send_data;

void setup()
{
	pinMode(LED, OUTPUT);

	TWE_Serial.begin(115200);
	Serial.begin(115200);
	delay(1000);

	Serial.println("---------------------");
	Serial.println("hello, this is master");
	Serial.println("---------------------");
	Serial.println("start!");
	Serial.println("");

	digitalWrite(LED, LOW);
}

void loop()
{

	//---------------Twe-LiteとCITRUSで設定--------------------
	byte recv = 0;
	byte pc = 9;

	while (TWE_Serial.available())
	{
		digitalWrite(LED, HIGH);
		recv = TWE_Serial.read();
		//Serial.write(recv);
		Serial.print((char)recv);
	}

	//while (Serial.available())
	//{
	//	digitalWrite(LED, HIGH);
	//	recv = Serial.read();
	//	//Serial.write(recv);
	//	TWE_Serial.write(recv);
	//}


	//----------PCからロボに送るstring----------
	// : 01 01 data(HEX) X \r\n
	// ~1 ~2 ~3        ~4~5   ~6
	// 
	// 1 ヘッダ
	// 2 ロボのアドレス
	// 3 UART設定
	// 4 送りたいデータ(16進)
	// 5 チェックサム(省略するのでX)
	// 6 改行コード

	while (Serial.available())
	{
		digitalWrite(LED, HIGH);
		pc = Serial.read();

		if (pc == 'A') // STOP_MODEへチェンジ
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("23"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == 'B') // CONTROL_MODEへチェンジ
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("25"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == 'C') // AUTO_MODEへチェンジ
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("27"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == 'D') // TEST_MODEへチェンジ
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("29"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == '0') // モータ前進
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("00"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == '1') // モータ後進
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("11"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == '9') // モータストップ
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("99"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == '4') // モータ右旋回
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("44"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == '6') // モータ左旋回
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("66"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == '2') // モータ右だけ前
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("31"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == '3') // モータ右だけ後
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("33"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == '7') // モータ左だけ前
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("51"); // コマンド
			TWE_Serial.print("X\r\n");
		}
		else if (pc == '8') // モータ左だけ後
		{
			TWE_Serial.print(":0101");
			TWE_Serial.print("53"); // コマンド
			TWE_Serial.print("X\r\n");
		}
	}

	digitalWrite(LED, LOW);
	//-----------------------------------------------------------

}

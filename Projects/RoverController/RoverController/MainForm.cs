using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoverController
{
    public partial class MainForm : Form
    {
        private string ReceiveData = null;
        private string LatData = null;
        private string LonData = null;
        private string TempData = null;

        // スレット処理用
        private delegate void Delegate_write(char data);

        public MainForm()
        {
            InitializeComponent();
            setSerialComboBox();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ComboBoxMode.SelectedIndex = 0;
        }

        // シリアルポート選択用ComboBox
        private void setSerialComboBox()
        {
            foreach (var portName in SerialPort.GetPortNames())
            {
                ComboBoxCom.Items.Add(portName);
            }
            if (ComboBoxCom.Items.Count > 0)
            {
                ComboBoxCom.SelectedIndex = 0;
                ComboBoxCom.Enabled = true;
            }
        }

        // シリアル通信開始
        private void SerialPortOpen()
        {
            string portName = ComboBoxCom.SelectedItem.ToString();
            serialPort.BaudRate = 115200;
            serialPort.PortName = portName;
            serialPort.Open();
        }

        // シリアル通信終了
        private void SerialPortClose()
        {
            serialPort.Close();
        }

        // データを読み取ったとき
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // 受信したデータ
            char data = (char)serialPort.ReadChar();
            // 異なるスレッドのテキストボックスに書き込む

            Invoke(new Delegate_write(Write), new Object[] { data });
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            if (ButtonOpen.Text == "Open")
            {
                SerialPortOpen();
                ButtonOpen.Text = "Close";
            }
            else
            {
                SerialPortClose();
                ButtonOpen.Text = "Open";
            }
        }

        // データを書き込む
        private void Write(char data)
        {
            TextRaw.AppendText(data + "\n");

            ReceiveData = ReceiveData + data;

            if(data == '\n')
            {
                //TextRaw.AppendText("\r\n" + ReceiveData + " OK!\r\n");
                
                // ReceiveDataにちょうど一行分入った
                // ここからストリング解析して緯度経度取り出す
                //----------------------------------------------
                int indexA = ReceiveData.IndexOf('A');
                int indexB = ReceiveData.IndexOf('B');
                int indexC = ReceiveData.IndexOf('C');
                int indexD = ReceiveData.IndexOf('D');

                int lengthLat = indexB - indexA - 1;
                int lengthLon = indexC - indexB - 1;
                int lengthTemp = indexD - indexC - 1;

                if (lengthLat > 0)
                {
                    LatData = ReceiveData.Substring(indexA + 1, lengthLat);
                }
                if (lengthLon > 0)
                {
                    LonData = ReceiveData.Substring(indexB + 1, indexC - indexB - 1);
                }
                if (lengthTemp > 0)
                {
                    TempData = ReceiveData.Substring(indexC + 1, indexD - indexC - 1);
                }

                // 緯度経度をintから少数に
                if (LatData != null && LatData.Length > 6)
                {
                    LatData = LatData.Insert(LatData.Length - 6, ".");
                }
                if (LonData != null && LonData.Length > 6)
                {
                    LonData = LonData.Insert(LonData.Length - 6, ".");
                }
                TextLatLon.AppendText(LatData + " / " + LonData + "\r\n");

                // 温度をintから少数に
                if (TempData != null && TempData.Length > 0)
                {
                    TempData = TempData.Insert(TempData.Length - 1, ".");
                }

                TextTemp.AppendText(TempData + "℃\r\n");

                //----------------------------------------------

                ReceiveData = null;
            }
        }

        private void serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            TextLatLon.AppendText("error\n");
        }
        


        private void ButtonForward_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("0");
            }
        }


        private void ButtonBack_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("1");
            }
        }

        private void ButtonBrake_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("9");
            }
        }

        private void ButtonTurnRight_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("4");
            }
        }

        private void ButtonTurnLeft_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("6");
            }
        }

        private void ButtonRightForward_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("2");
            }
        }

        private void ButtonLeftForward_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("7");
            }
        }

        private void ButtonRightBack_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("3");
            }
        }

        private void ButtonLeftBack_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("8");
            }
        }

        private void ComboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                if (ComboBoxMode.SelectedItem.ToString() == "STOP_MODE")
                {
                    serialPort.Write("A");
                }
                else if (ComboBoxMode.SelectedItem.ToString() == "CONTROL_MODE")
                {
                    serialPort.Write("B");
                }
                else if (ComboBoxMode.SelectedItem.ToString() == "AUTO_MODE")
                {
                    serialPort.Write("C");
                }
                else if (ComboBoxMode.SelectedItem.ToString() == "TEST_MODE")
                {
                    serialPort.Write("D");
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // シリアル通信していた場合は、閉じてから終了する
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }


    }
}

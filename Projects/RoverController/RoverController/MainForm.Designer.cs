
namespace RoverController
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ComboBoxCom = new System.Windows.Forms.ComboBox();
            this.TextTemp = new System.Windows.Forms.TextBox();
            this.TextLatLon = new System.Windows.Forms.TextBox();
            this.LabelLatLon = new System.Windows.Forms.Label();
            this.LabelTemp = new System.Windows.Forms.Label();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.ButtonOpen = new System.Windows.Forms.Button();
            this.TextRaw = new System.Windows.Forms.TextBox();
            this.ButtonForward = new System.Windows.Forms.Button();
            this.ButtonBack = new System.Windows.Forms.Button();
            this.ButtonBrake = new System.Windows.Forms.Button();
            this.ButtonTurnRight = new System.Windows.Forms.Button();
            this.ButtonTurnLeft = new System.Windows.Forms.Button();
            this.ComboBoxMode = new System.Windows.Forms.ComboBox();
            this.LabelMode = new System.Windows.Forms.Label();
            this.LabelPort = new System.Windows.Forms.Label();
            this.ButtonRightForward = new System.Windows.Forms.Button();
            this.ButtonLeftForward = new System.Windows.Forms.Button();
            this.ButtonLeftBack = new System.Windows.Forms.Button();
            this.ButtonRightBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ComboBoxCom
            // 
            this.ComboBoxCom.FormattingEnabled = true;
            this.ComboBoxCom.Location = new System.Drawing.Point(858, 33);
            this.ComboBoxCom.Name = "ComboBoxCom";
            this.ComboBoxCom.Size = new System.Drawing.Size(121, 26);
            this.ComboBoxCom.TabIndex = 0;
            // 
            // TextTemp
            // 
            this.TextTemp.Location = new System.Drawing.Point(317, 33);
            this.TextTemp.Multiline = true;
            this.TextTemp.Name = "TextTemp";
            this.TextTemp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextTemp.Size = new System.Drawing.Size(203, 239);
            this.TextTemp.TabIndex = 1;
            // 
            // TextLatLon
            // 
            this.TextLatLon.Location = new System.Drawing.Point(12, 33);
            this.TextLatLon.Multiline = true;
            this.TextLatLon.Name = "TextLatLon";
            this.TextLatLon.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextLatLon.Size = new System.Drawing.Size(279, 239);
            this.TextLatLon.TabIndex = 2;
            // 
            // LabelLatLon
            // 
            this.LabelLatLon.AutoSize = true;
            this.LabelLatLon.Location = new System.Drawing.Point(20, 12);
            this.LabelLatLon.Name = "LabelLatLon";
            this.LabelLatLon.Size = new System.Drawing.Size(89, 18);
            this.LabelLatLon.TabIndex = 3;
            this.LabelLatLon.Text = "緯度/経度";
            // 
            // LabelTemp
            // 
            this.LabelTemp.AutoSize = true;
            this.LabelTemp.Location = new System.Drawing.Point(325, 12);
            this.LabelTemp.Name = "LabelTemp";
            this.LabelTemp.Size = new System.Drawing.Size(44, 18);
            this.LabelTemp.TabIndex = 4;
            this.LabelTemp.Text = "温度";
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 115200;
            this.serialPort.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort_ErrorReceived);
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort_DataReceived);
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.Location = new System.Drawing.Point(998, 28);
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.Size = new System.Drawing.Size(81, 34);
            this.ButtonOpen.TabIndex = 5;
            this.ButtonOpen.Text = "Open";
            this.ButtonOpen.UseVisualStyleBackColor = true;
            this.ButtonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
            // 
            // TextRaw
            // 
            this.TextRaw.Location = new System.Drawing.Point(12, 297);
            this.TextRaw.Multiline = true;
            this.TextRaw.Name = "TextRaw";
            this.TextRaw.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextRaw.Size = new System.Drawing.Size(508, 250);
            this.TextRaw.TabIndex = 6;
            // 
            // ButtonForward
            // 
            this.ButtonForward.Location = new System.Drawing.Point(577, 90);
            this.ButtonForward.Name = "ButtonForward";
            this.ButtonForward.Size = new System.Drawing.Size(482, 50);
            this.ButtonForward.TabIndex = 7;
            this.ButtonForward.Text = "↑";
            this.ButtonForward.UseVisualStyleBackColor = true;
            this.ButtonForward.Click += new System.EventHandler(this.ButtonForward_Click);
            // 
            // ButtonBack
            // 
            this.ButtonBack.Location = new System.Drawing.Point(577, 396);
            this.ButtonBack.Name = "ButtonBack";
            this.ButtonBack.Size = new System.Drawing.Size(482, 50);
            this.ButtonBack.TabIndex = 8;
            this.ButtonBack.Text = "↓";
            this.ButtonBack.UseVisualStyleBackColor = true;
            this.ButtonBack.Click += new System.EventHandler(this.ButtonBack_Click);
            // 
            // ButtonBrake
            // 
            this.ButtonBrake.Location = new System.Drawing.Point(676, 484);
            this.ButtonBrake.Name = "ButtonBrake";
            this.ButtonBrake.Size = new System.Drawing.Size(296, 63);
            this.ButtonBrake.TabIndex = 9;
            this.ButtonBrake.Text = "停止";
            this.ButtonBrake.UseVisualStyleBackColor = true;
            this.ButtonBrake.Click += new System.EventHandler(this.ButtonBrake_Click);
            // 
            // ButtonTurnRight
            // 
            this.ButtonTurnRight.Location = new System.Drawing.Point(954, 156);
            this.ButtonTurnRight.Name = "ButtonTurnRight";
            this.ButtonTurnRight.Size = new System.Drawing.Size(105, 223);
            this.ButtonTurnRight.TabIndex = 10;
            this.ButtonTurnRight.Text = "↓旋回↑";
            this.ButtonTurnRight.UseVisualStyleBackColor = true;
            this.ButtonTurnRight.Click += new System.EventHandler(this.ButtonTurnRight_Click);
            // 
            // ButtonTurnLeft
            // 
            this.ButtonTurnLeft.Location = new System.Drawing.Point(577, 156);
            this.ButtonTurnLeft.Name = "ButtonTurnLeft";
            this.ButtonTurnLeft.Size = new System.Drawing.Size(105, 223);
            this.ButtonTurnLeft.TabIndex = 11;
            this.ButtonTurnLeft.Text = "↑旋回↓";
            this.ButtonTurnLeft.UseVisualStyleBackColor = true;
            this.ButtonTurnLeft.Click += new System.EventHandler(this.ButtonTurnLeft_Click);
            // 
            // ComboBoxMode
            // 
            this.ComboBoxMode.FormattingEnabled = true;
            this.ComboBoxMode.Items.AddRange(new object[] {
            "STOP_MODE",
            "CONTROL_MODE",
            "AUTO_MODE",
            "TEST_MODE"});
            this.ComboBoxMode.Location = new System.Drawing.Point(554, 33);
            this.ComboBoxMode.Name = "ComboBoxMode";
            this.ComboBoxMode.Size = new System.Drawing.Size(183, 26);
            this.ComboBoxMode.TabIndex = 12;
            this.ComboBoxMode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMode_SelectedIndexChanged);
            // 
            // LabelMode
            // 
            this.LabelMode.AutoSize = true;
            this.LabelMode.Location = new System.Drawing.Point(563, 12);
            this.LabelMode.Name = "LabelMode";
            this.LabelMode.Size = new System.Drawing.Size(56, 18);
            this.LabelMode.TabIndex = 13;
            this.LabelMode.Text = "MODE";
            // 
            // LabelPort
            // 
            this.LabelPort.AutoSize = true;
            this.LabelPort.Location = new System.Drawing.Point(866, 9);
            this.LabelPort.Name = "LabelPort";
            this.LabelPort.Size = new System.Drawing.Size(106, 18);
            this.LabelPort.TabIndex = 14;
            this.LabelPort.Text = "シリアルポート";
            // 
            // ButtonRightForward
            // 
            this.ButtonRightForward.Location = new System.Drawing.Point(831, 156);
            this.ButtonRightForward.Name = "ButtonRightForward";
            this.ButtonRightForward.Size = new System.Drawing.Size(105, 97);
            this.ButtonRightForward.TabIndex = 15;
            this.ButtonRightForward.Text = "↑";
            this.ButtonRightForward.UseVisualStyleBackColor = true;
            this.ButtonRightForward.Click += new System.EventHandler(this.ButtonRightForward_Click);
            // 
            // ButtonLeftForward
            // 
            this.ButtonLeftForward.Location = new System.Drawing.Point(699, 156);
            this.ButtonLeftForward.Name = "ButtonLeftForward";
            this.ButtonLeftForward.Size = new System.Drawing.Size(105, 97);
            this.ButtonLeftForward.TabIndex = 16;
            this.ButtonLeftForward.Text = "↑";
            this.ButtonLeftForward.UseVisualStyleBackColor = true;
            this.ButtonLeftForward.Click += new System.EventHandler(this.ButtonLeftForward_Click);
            // 
            // ButtonLeftBack
            // 
            this.ButtonLeftBack.Location = new System.Drawing.Point(699, 282);
            this.ButtonLeftBack.Name = "ButtonLeftBack";
            this.ButtonLeftBack.Size = new System.Drawing.Size(105, 97);
            this.ButtonLeftBack.TabIndex = 17;
            this.ButtonLeftBack.Text = "↓";
            this.ButtonLeftBack.UseVisualStyleBackColor = true;
            this.ButtonLeftBack.Click += new System.EventHandler(this.ButtonLeftBack_Click);
            // 
            // ButtonRightBack
            // 
            this.ButtonRightBack.Location = new System.Drawing.Point(831, 282);
            this.ButtonRightBack.Name = "ButtonRightBack";
            this.ButtonRightBack.Size = new System.Drawing.Size(105, 97);
            this.ButtonRightBack.TabIndex = 18;
            this.ButtonRightBack.Text = "↓";
            this.ButtonRightBack.UseVisualStyleBackColor = true;
            this.ButtonRightBack.Click += new System.EventHandler(this.ButtonRightBack_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 572);
            this.Controls.Add(this.ButtonRightBack);
            this.Controls.Add(this.ButtonLeftBack);
            this.Controls.Add(this.ButtonLeftForward);
            this.Controls.Add(this.ButtonRightForward);
            this.Controls.Add(this.LabelPort);
            this.Controls.Add(this.LabelMode);
            this.Controls.Add(this.ComboBoxMode);
            this.Controls.Add(this.ButtonTurnLeft);
            this.Controls.Add(this.ButtonTurnRight);
            this.Controls.Add(this.ButtonBrake);
            this.Controls.Add(this.ButtonBack);
            this.Controls.Add(this.ButtonForward);
            this.Controls.Add(this.TextRaw);
            this.Controls.Add(this.ButtonOpen);
            this.Controls.Add(this.LabelTemp);
            this.Controls.Add(this.LabelLatLon);
            this.Controls.Add(this.TextLatLon);
            this.Controls.Add(this.TextTemp);
            this.Controls.Add(this.ComboBoxCom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "RoverController";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBoxCom;
        private System.Windows.Forms.TextBox TextTemp;
        private System.Windows.Forms.TextBox TextLatLon;
        private System.Windows.Forms.Label LabelLatLon;
        private System.Windows.Forms.Label LabelTemp;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Button ButtonOpen;
        private System.Windows.Forms.TextBox TextRaw;
        private System.Windows.Forms.Button ButtonForward;
        private System.Windows.Forms.Button ButtonBack;
        private System.Windows.Forms.Button ButtonBrake;
        private System.Windows.Forms.Button ButtonTurnRight;
        private System.Windows.Forms.Button ButtonTurnLeft;
        private System.Windows.Forms.ComboBox ComboBoxMode;
        private System.Windows.Forms.Label LabelMode;
        private System.Windows.Forms.Label LabelPort;
        private System.Windows.Forms.Button ButtonRightForward;
        private System.Windows.Forms.Button ButtonLeftForward;
        private System.Windows.Forms.Button ButtonLeftBack;
        private System.Windows.Forms.Button ButtonRightBack;
    }
}


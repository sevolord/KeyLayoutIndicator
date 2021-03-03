namespace KeyIndicator
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.BWriteText = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CBComs = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lComStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lLangStatus = new System.Windows.Forms.Label();
            this.lLang = new System.Windows.Forms.Label();
            this.bSetPort = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.PortName = "COM11";
            this.serialPort1.ReadTimeout = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bSetPort);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.BWriteText);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.CBComs);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 364);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройка com-порта";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // BWriteText
            // 
            this.BWriteText.Location = new System.Drawing.Point(6, 148);
            this.BWriteText.Name = "BWriteText";
            this.BWriteText.Size = new System.Drawing.Size(102, 23);
            this.BWriteText.TabIndex = 9;
            this.BWriteText.Text = "Записать текст";
            this.BWriteText.UseVisualStyleBackColor = true;
            this.BWriteText.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 122);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(263, 20);
            this.textBox1.TabIndex = 7;
            // 
            // CBComs
            // 
            this.CBComs.FormattingEnabled = true;
            this.CBComs.Location = new System.Drawing.Point(6, 19);
            this.CBComs.Name = "CBComs";
            this.CBComs.Size = new System.Drawing.Size(121, 21);
            this.CBComs.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lComStatus);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lLangStatus);
            this.groupBox2.Controls.Add(this.lLang);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(310, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 364);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Состояние";
            // 
            // lComStatus
            // 
            this.lComStatus.AutoSize = true;
            this.lComStatus.Location = new System.Drawing.Point(120, 35);
            this.lComStatus.Name = "lComStatus";
            this.lComStatus.Size = new System.Drawing.Size(35, 13);
            this.lComStatus.TabIndex = 3;
            this.lComStatus.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Состояние порта";
            // 
            // lLangStatus
            // 
            this.lLangStatus.AutoSize = true;
            this.lLangStatus.Location = new System.Drawing.Point(120, 22);
            this.lLangStatus.Name = "lLangStatus";
            this.lLangStatus.Size = new System.Drawing.Size(35, 13);
            this.lLangStatus.TabIndex = 1;
            this.lLangStatus.Text = "label1";
            // 
            // lLang
            // 
            this.lLang.AutoSize = true;
            this.lLang.Location = new System.Drawing.Point(6, 22);
            this.lLang.Name = "lLang";
            this.lLang.Size = new System.Drawing.Size(112, 13);
            this.lLang.TabIndex = 0;
            this.lLang.Text = "Активная раскладка";
            // 
            // bSetPort
            // 
            this.bSetPort.Location = new System.Drawing.Point(150, 19);
            this.bSetPort.Name = "bSetPort";
            this.bSetPort.Size = new System.Drawing.Size(75, 23);
            this.bSetPort.TabIndex = 11;
            this.bSetPort.Text = "Установить";
            this.bSetPort.UseVisualStyleBackColor = true;
            this.bSetPort.Click += new System.EventHandler(this.bSetPort_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 364);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CBComs;
        private System.Windows.Forms.Button BWriteText;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lComStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lLangStatus;
        private System.Windows.Forms.Label lLang;
        private System.Windows.Forms.Button bSetPort;
    }
}


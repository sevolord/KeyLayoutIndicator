
namespace KeyLayoutIndicator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BReScan = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.BDisconnect = new System.Windows.Forms.Button();
            this.lLangStatus = new System.Windows.Forms.Label();
            this.labelSost = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BConnect = new System.Windows.Forms.Button();
            this.CBComs = new System.Windows.Forms.ComboBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM4";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BReScan);
            this.groupBox1.Controls.Add(this.BDisconnect);
            this.groupBox1.Controls.Add(this.lLangStatus);
            this.groupBox1.Controls.Add(this.labelSost);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BConnect);
            this.groupBox1.Controls.Add(this.CBComs);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 148);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки";
            // 
            // BReScan
            // 
            this.BReScan.ImageIndex = 0;
            this.BReScan.ImageList = this.imageList1;
            this.BReScan.Location = new System.Drawing.Point(117, 19);
            this.BReScan.Name = "BReScan";
            this.BReScan.Size = new System.Drawing.Size(25, 23);
            this.BReScan.TabIndex = 7;
            this.BReScan.UseVisualStyleBackColor = true;
            this.BReScan.Click += new System.EventHandler(this.BReScan_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "reload.png");
            // 
            // BDisconnect
            // 
            this.BDisconnect.Location = new System.Drawing.Point(12, 75);
            this.BDisconnect.Name = "BDisconnect";
            this.BDisconnect.Size = new System.Drawing.Size(130, 23);
            this.BDisconnect.TabIndex = 6;
            this.BDisconnect.Text = "Отключится";
            this.BDisconnect.UseVisualStyleBackColor = true;
            this.BDisconnect.Click += new System.EventHandler(this.BDisconnect_Click);
            // 
            // lLangStatus
            // 
            this.lLangStatus.AutoSize = true;
            this.lLangStatus.Location = new System.Drawing.Point(79, 125);
            this.lLangStatus.Name = "lLangStatus";
            this.lLangStatus.Size = new System.Drawing.Size(63, 13);
            this.lLangStatus.TabIndex = 5;
            this.lLangStatus.Text = "lLangStatus";
            // 
            // labelSost
            // 
            this.labelSost.AutoSize = true;
            this.labelSost.Location = new System.Drawing.Point(79, 103);
            this.labelSost.Name = "labelSost";
            this.labelSost.Size = new System.Drawing.Size(50, 13);
            this.labelSost.TabIndex = 4;
            this.labelSost.Text = "labelSost";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Язык:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Состояние:";
            // 
            // BConnect
            // 
            this.BConnect.Location = new System.Drawing.Point(12, 46);
            this.BConnect.Name = "BConnect";
            this.BConnect.Size = new System.Drawing.Size(130, 23);
            this.BConnect.TabIndex = 1;
            this.BConnect.Text = "Подключится";
            this.BConnect.UseVisualStyleBackColor = true;
            this.BConnect.Click += new System.EventHandler(this.BConnect_Click);
            // 
            // CBComs
            // 
            this.CBComs.FormattingEnabled = true;
            this.CBComs.Location = new System.Drawing.Point(12, 19);
            this.CBComs.Name = "CBComs";
            this.CBComs.Size = new System.Drawing.Size(99, 21);
            this.CBComs.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(154, 148);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Индикатор раскладки";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CBComs;
        private System.Windows.Forms.Button BConnect;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label lLangStatus;
        private System.Windows.Forms.Label labelSost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BDisconnect;
        private System.Windows.Forms.Button BReScan;
        private System.Windows.Forms.ImageList imageList1;
    }
}


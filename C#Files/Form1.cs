using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;



namespace KeyIndicator
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {

            InitializeComponent();
            CBComs.Items.Clear();
            // Получаем список COM портов доступных в системе
            string[] portnames = SerialPort.GetPortNames();
            // Проверяем есть ли доступные
            if (portnames.Length == 0)
            {
                MessageBox.Show("COM PORT not found");
            }
            foreach (string portName in portnames)
            {
                //добавляем доступные COM порты в список           
                CBComs.Items.Add(portName);
                Console.WriteLine(portnames.Length);
                if (portnames[0] != null)
                {
                    CBComs.SelectedItem = portnames[0];
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InputLanguage myCurrentLanguage = InputLanguage.CurrentInputLanguage;

            if (myCurrentLanguage != null)
                textBox1.Text = "Layout: " + myCurrentLanguage.LayoutName;
            else
                textBox1.Text = "There is no current language";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                textBox1.Text += lang.Culture.EnglishName + '\n';
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PortWrite(textBox1.Text);
        }
        void PortWrite(String txt)
        {
            try 
            { 
                serialPort1.Open();
                lComStatus.Text = "Работает";
                //System.Threading.Thread.Sleep(100);
                // небольшая пауза, ведь SerialPort не терпит суеты
                serialPort1.Write(txt);
                serialPort1.Close();
            }
            catch
            {
                lComStatus.Text = "Ошибка открытия порта";
            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            InputLanguage myCurrentLanguage = InputLanguage.CurrentInputLanguage;
            if (myCurrentLanguage != null )
            {
                
                    String txt = myCurrentLanguage.LayoutName;
                    if (txt == "Русская")
                    {
                        PortWrite("1");
                    }
                    if (txt == "США")
                    {
                        PortWrite("3");
                    }
                lLangStatus.Text = txt;


            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }

        private void bSetPort_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = CBComs.Text;
        }
    }
}

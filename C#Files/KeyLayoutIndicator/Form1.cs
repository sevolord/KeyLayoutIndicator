﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports; //подключаем класс ком-порта
using System.Threading;
using System.Threading.Tasks; // подключаем для таймера
using System.Windows.Forms;
using System.Runtime.InteropServices; // подключаем для использования DllImport 

namespace KeyLayoutIndicator
{
    public partial class Form1 : Form
    {
        // задаем переменные класса
        SerialPort currentPort;    // глобально объявляем используемы порт, т.к. будем обращаться из разных методов
        String CurrentLayout = ""; // переменная с текущей раскладкой
        bool CurrentCaps = false, CurrentNum = false, CurrentScr = false; // переменные с текущими состояниями lock-ов
        bool minimized = false;    // переменная-флаг, содержащая инф-у о то, что окно свернуто
        System.Threading.Timer timer;   // создаем таймер 
        string oldDataString = "";
        int defaultBrightness = 30; //яркость по умолчанию
        int defaultMethod = 1;  
        /*  метод 1, передача только состояния, без настроек:
         * вид принимаемой строки: 1,2,3,4,5,6,7;
         *  где: 1 - тип параметра, 2 - первый светодиод обращения, 3 - второй светодиод обращения
         *  4 - третий, 5 - четвертый, 6 - пятый, 7 - Brightness 
         */
        int oldLang = 0; //для запоминания прошлой раскладки

        public Form1() // метод создания формы
        {
            InitializeComponent();
            BDisconnect.Enabled = false; //отключаем кнопку отключения, дабы не разрывать несуществующее соединение
            this.Resize += new System.EventHandler(this.Form1_Resize);  // добавляем событие на изменение окна
            notifyIcon1.Visible = false;    // прячем иконку в трее, пока программа не свернута
            ComPortScan(); // сканируем список компортов и добавляем их в комбобокс
        }

        void ComPortScan()  //метод сканирования ком-портов и добавления их комбо-бокс
        {
            CBComs.Items.Clear(); //чистим список COM портов - необходимо, когда вызывается на кнопке "обновить список"            
            string[] portnames = SerialPort.GetPortNames(); // Получаем список COM портов доступных в системе
            if (portnames.Length == 0) MessageBox.Show("COM PORT not found");   // Проверяем есть ли доступные порты, если нет, выводим сообщение что не найдены    
            foreach (string portName in portnames) // прочесываем массив с именами компортов и добавляем их в комбобокс
            {
                CBComs.Items.Add(portName); //обновляем item 
                if (portnames[0] != null) CBComs.SelectedItem = portnames[0]; // если значение есть, выбираем первое значение
            }
        }
        private void BReScan_Click(object sender, EventArgs e)  //кнопка "обновить список"       
        {
            ComPortScan();
        }

        private void BConnect_Click(object sender, EventArgs e) // нажатие кнопки "подключить"
        {

            try //обработчик на случай отсутствующих, неправильных значений и др. ошибок подключения
            {
                currentPort = new SerialPort(CBComs.Text, 115200);    //создаем порт с названием из комбобокса и скорость 9600 бод
                currentPort.DtrEnable = true;   // включаем индикатор готовности
                currentPort.Open(); // открываем порт
                labelSost.Text = "Подключен";
                timer = new System.Threading.Timer(TimerTick, null, 500, 50); // создаем и запускаем таймер
                BDisconnect.Enabled = true; // отключение становится активно
                BConnect.Enabled = false; // делаем кнопку подключения неактивной
            }
            catch
            {
                labelSost.Text = "Ошибка открытия порта";
            }
        }

        private void BDisconnect_Click(object sender, EventArgs e)
        {
            currentPort.Close();    // закрываем порт
            labelSost.Text = "Отключен";
            BConnect.Enabled = true; // делаем кнопку подключения активной
            BDisconnect.Enabled = false; // отключение - наоборот 
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            // проверяем наше окно, и если оно было свернуто, делаем событие        
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false; // прячем наше окно из панели
                notifyIcon1.Visible = true; // делаем нашу иконку в трее активной
                minimized = true;   // включаем флаг о том, что свернули программу
            }
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) // по двойному щелчку разворачиваем окно 
        {
            notifyIcon1.Visible = false;    // делаем нашу иконку скрытой
            this.ShowInTaskbar = true;  // возвращаем отображение окна в панели
            WindowState = FormWindowState.Normal;   //разворачиваем окно
            minimized = false;  // выключаем флаг о том, что свернули программу
        }

        void TimerTick(object sender) // выполняется каждый "тик" таймера
        {
            if (minimized) this.Invoke(new Action(() => GetLayOut())); //вызов из свернутого состояния, для определения раскладки другого окна
            else this.Invoke(new Action(() => GetLayOutInForm()));// вызов, если активно окно нашей программы
            // используется Invoke из-за того, что форма и объекты из кода рождаются в разных процессах, Invoke нужен для доступа одногопроцесса в другой

        }
        void GetLayOutInForm() // метод получения раскладки, если активное окно - наша программа. не надо получать хэндл активного окна и вот это вот все
        {
            string dataString = "";
            dataString += defaultMethod; dataString += ",";
            // 1 - метод, 2 - ins, 3 - scroll, 4 - caps, 5 - num, 6 - lang, 7 - brigness

            bool ins = KeyboardLayoutTools.GetKeyS(Keys.Insert);
            //if (ins) LCaps.Text = "Вкл";
            //else LCaps.Text = "Выкл";
            dataString += ins ? 1 : 0; dataString += ",";

            bool scroll = KeyboardLayoutTools.GetKeyS(Keys.Scroll);
            if (scroll) LScr.Text = "Вкл";
            else LScr.Text = "Выкл";
            dataString += scroll ? 1 : 0; dataString += ",";

            bool caps = KeyboardLayoutTools.GetKeyS(Keys.CapsLock);
            if (caps) LCaps.Text = "Вкл";
            else LCaps.Text = "Выкл";
            dataString += caps ? 1 : 0; dataString += ",";

            bool num = KeyboardLayoutTools.GetKeyS(Keys.NumLock);
            if (num) LNum.Text = "Вкл";
            else LNum.Text = "Выкл";
            dataString += num ? 1 : 0; dataString += ",";
            

            InputLanguage myCurrentLanguage = InputLanguage.CurrentInputLanguage; // получаем активную раскладку клавиатуры
            if (myCurrentLanguage != null) // если раскладка была получена
            {
                String txt = myCurrentLanguage.LayoutName; // получаем имя раскладки
                if (txt == "Русская")   // если имя русская
                {
                    //PortWrite("1,0,2,0,255,0,150;");     // пишем в порт зеленый цвет
                    lLangStatus.Text = "RU";    // пишем в лабель
                    dataString += 0; dataString += ",";
                }
                else if (txt == "США")  // если имя раскладки США
                {
                    //PortWrite("1,0,2,0,0,250,150;");     // пишем в порт синий цвет
                    lLangStatus.Text = "EN";    // пишем в лабель
                    dataString += 1; dataString += ",";
                }
            }
            dataString += defaultBrightness;
            if (!oldDataString.Equals(dataString))
            {
                PortWrite(dataString);
                oldDataString = dataString;
            }

        }
        void GetLayOut() // метод определения раскладки активного окна 
        {
            string dataString = "";
            dataString += defaultMethod; dataString += ",";
            // 1 - метод, 2 - ins, 3 - scroll, 4 - caps, 5 - num, 6 - lang, 7 - brigness

            bool ins = KeyboardLayoutTools.GetKeyS(Keys.Insert);
            dataString += ins ? 1 : 0; dataString += ",";

            bool scroll = KeyboardLayoutTools.GetKeyS(Keys.Scroll);
            dataString += scroll ? 1 : 0; dataString += ",";

            bool caps = KeyboardLayoutTools.GetKeyS(Keys.CapsLock);
            dataString += caps ? 1 : 0; dataString += ",";

            bool num = KeyboardLayoutTools.GetKeyS(Keys.NumLock);
            dataString += num ? 1 : 0; dataString += ",";
            

            IntPtr selectedWindow = KeyboardLayoutTools.NativeMethods.GetForegroundWindow(); // получаем id активного окна
            int currId;
            if (KeyboardLayoutTools.CheckKeyboardLayout(Handle, selectedWindow, out currId)) // функция, пишущая в указанную переменную currID раскладку, если она была изменена
            {
                int myIDLang = currId == 1033 ? 1 : 0;// английский 
                oldLang = myIDLang;
            }
            else
            {
                dataString += oldLang; dataString += ","; // не удалось получить язык, подсталяем что было
            }

            dataString += defaultBrightness;
            if (!oldDataString.Equals(dataString)) 
            {
                PortWrite(dataString);
                oldDataString = dataString;
            }

        }

        void PortWrite(String txt) //функция записи переданного параметра в порт
        {
            try
            {
                //System.Threading.Thread.Sleep(100); // небольшая пауза, ведь SerialPort не терпит суеты
                currentPort.Write(txt);
            }
            catch
            {
                if (!minimized) labelSost.Text = "Ошибка записи в порт";
            }

        }


    }
    public class KeyboardLayoutTools // класс для работы с раскладкой указанных окон
    {

        private static readonly InputLanguageCollection InstalledInputLanguages = InputLanguage.InstalledInputLanguages; // получаем список установленных в системе языков
        private static int _lastKeyLayout = -1; // переменная, где будем хранить прошлое значение раскладки 


        public static int GetKeyboardLayoutId(IntPtr windowHandle)      //метод возвращает id раскладки переданного окна
        {
            int lpdwProcessId;
            int winThreadProcId = NativeMethods.GetWindowThreadProcessId(windowHandle, out lpdwProcessId); // получаем id окна
            IntPtr keybLayout = NativeMethods.GetKeyboardLayout(winThreadProcId);   // получаем раскладку окна

            for (int i = 0; i < InstalledInputLanguages.Count; i++)     // перебираем список языков, установленных в системе
                if (keybLayout == InstalledInputLanguages[i].Handle)    // если совпадают раскладка окна и указанный в системе язык 
                    return InstalledInputLanguages[i].Culture.KeyboardLayoutId;     // возвращаем id раскладки 

            return -1;      // если такой раскладки нет, возвращаем -1
        }

        public static bool CheckKeyboardLayout(IntPtr thisWindow, IntPtr selectedWindow, out int currentLayId) // передаем id нашей программы, id активного окна, указатель на переменную с id раскладки
        {
            currentLayId = _lastKeyLayout;      // прошлая раскладка 
            if (selectedWindow != thisWindow)   // если выбранное окно - это не созданная нами форма
            {
                int foregroundWindowInput = GetKeyboardLayoutId(selectedWindow); // получаем id раскладки указанного окна
                if (foregroundWindowInput == _lastKeyLayout) return false;  // если совпадают с прошлым значением, возвращаем false, язык не был переключен

                int thisWindowInput = GetKeyboardLayoutId(thisWindow);  // раскладка нашего окна
                currentLayId = _lastKeyLayout = thisWindowInput;        // запоминаем текущую раскладку в перменную в переменную

                if (thisWindowInput != foregroundWindowInput)           // если раскладка нашего окна не сопадает с раскладкой активного окна, меняем его. Нужно для того, чтобы работало на windows 7 и ниже.
                {
                    NativeMethods.ActivateKeyboardLayout(new IntPtr(1), 8); //активируем указанную раскладку клавиатуры
                    return true;    // возвращаем значение, что раскадка изменилась
                }
                NativeMethods.ActivateKeyboardLayout(new IntPtr(1), 8); //активируем указанную раскладку клавиатуры
            }
            return true; //всегда возвращаем true //возращаем false, не переключаем на своей форме
        }

        public static bool GetKeyS(Keys myKey) 
        {
            bool keyState;
            int numState = NativeMethods.GetKeyState(myKey);
            if (numState > 0) keyState = true;
            else keyState = false;
            return keyState;
        }

        #region Nested type: NativeMethods

        public static class NativeMethods
        {
            /// 
            /// Получает маркер активного окна Windows.
            /// 
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetForegroundWindow();

            /// 
            /// Получает идентификационный номер потока для окна.
            /// 
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern int GetWindowThreadProcessId(IntPtr handleWindow, out int lpdwProcessId);

            /// 
            /// Получает информацию о раскладке клавиатуры.
            /// 
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetKeyboardLayout(int windowsThreadProcessId);

            /// 
            /// Активирует раскладку клавиатуры.
            /// 
            [DllImport("user32.dll")]
            public static extern int ActivateKeyboardLayout(IntPtr nkl, uint flags);

            /// 
            /// Получаем состояие клавиши.
            ///
            [DllImport("USER32.DLL")]
            public static extern Int16 GetKeyState(Keys keys);


        }

        #endregion
    }
}

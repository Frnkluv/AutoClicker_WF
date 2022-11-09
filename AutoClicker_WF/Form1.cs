using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClicker_WF
{
    public partial class Form1 : Form
    {
        // хук, который перехватывает нажатие клавиши
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        const uint LeftDown = 0x0002;   // нажатие на лкм
        const uint LeftUp = 0x0004;     // поднятие(отпускание) лкм

        private bool PressButton { get; set; } = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Update.Start();
            Clicker.Stop();
            label1.Text = trackBar1.Value.ToString();
        }

        // Start
        private void Start_Click(object sender, EventArgs e)
        {
            //Clicker.Start();

            if (sender is Button startButton)
            {
                
                if (PressButton)
                {
                    Clicker.Start();
                    startButton.BackColor = Color.LightGreen;
                    PressButton = false;
                    Start.Text = "ON";
                }
                else
                {
                    Clicker.Stop();
                    startButton.BackColor = Color.Snow;     //нет привязки на дефолтный цвет. Если изм в форме, то вручную менять тут.
                    PressButton = true;
                    Start.Text = "Press to start";
                }
            }
        }

        // Stop
        //private void Stop_Click(object sender, EventArgs e)
        //{
        //    Clicker.Stop();
        //}

        private void Update_Tick(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
            Clicker.Interval = trackBar1.Value;
        }

        private void Clicker_Tick(object sender, EventArgs e)
        {
            // если убрать локер то я хер выключу, из-за мильона кликов
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                mouse_event(LeftDown, 0, 0, 0, (UIntPtr)0);
                mouse_event(LeftUp, 0, 0, 0, (UIntPtr)0);
            }
            else return;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace littleGame
{
    public partial class Form1 : Form
    {
        private ArrayList _nums;
        private Random rnd;
        private int cClick;
        private int TimerVal;

        public Form1()
        {
            InitializeComponent();
            _nums = new ArrayList();
            rnd = new Random();
            TimerVal = 0;
            cClick = 0;
            foreach (Control el in Nums.Controls)
            {
                el.Click += ValidClick;
            }
        }

        private void ValidClick(object sender, EventArgs e)
        {
            cClick++;
            int num = int.Parse(((Control)sender).Text);
            if (_nums.IndexOf(num) == 0)
            {
                _nums.RemoveAt(0);
                ((Control)sender).Enabled = false;
                tBar.Value += 1;
            }
            if (_nums.Count == 0)
            {
                StopTimer();
                double rez = cClick == 10 ? 10 : (100 / (cClick - 10));
                sHit.Text = rez.ToString() + "%";
                sTime.Text = TimerVal.ToString() + " msec";
                MessageBox.Show("You Win!", "");
            }
            
        }

        private void InitButtons()
        {
            _nums.Clear();
            int n;
            
            for (int i = 0; i < 10; i++)
            {
                do 
                {
                    n = rnd.Next(0, 100);
                }while(_nums.IndexOf(n) != -1);
                _nums.Add(n);
            }

            var buttons = Nums.Controls.OfType<Button>();
            int idx = 0;
            foreach (Button el in buttons)
            {
                el.Text = String.Format("{0}", _nums[idx++]);
            }
            _nums.Sort();
        }



        private void button13_Click(object sender, EventArgs e)
        {
            InitButtons();
            timer.Interval = (int)Time.Value / 10;   
           
            foreach (Control el in Nums.Controls)
            {
                el.Enabled = true;
            }
            ClearData();
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            
            int n = (int)Time.Value / 10;
            TimerVal += n;
            if (TimerVal <= (int)Time.Value)
            {
                  pBar.Value += 10;
            }
            else
            {
                StopTimer();
                MessageBox.Show("You Lose!", "");
            }        
        }
        private void ClearData(){
            button13.Enabled = false;
            tBar.Value = 0;
            pBar.Value = 0;
            cClick = 0;
            sHit.Text = "";
            sTime.Text = "";
            TimerVal = 0;
        }

        private void StopTimer()
        {
            timer.Stop();
            button13.Enabled = true;
            foreach (Control el in Nums.Controls)
            {
                el.Enabled = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Automator
{
    public partial class Timer : Form
    {
        private Stopwatch timer = new Stopwatch();
        private float time;

        public Timer()
        {
            InitializeComponent();
        }

        private void clickBtn_MouseDown(object sender, MouseEventArgs e)
        {
            resetTimer();
        }

        private void clickBtn_KeyDown(object sender, KeyEventArgs e)
        {
            resetTimer();
        }

        private void resetTimer() {
            time = (time * 3 + timer.ElapsedMilliseconds) / 4;
            clickBtn.Text = (Math.Floor(time)/1000).ToString() + " Seconds";
            clickBtn.Text += "\n"+Math.Floor(time).ToString() + " Milliseconds";
            timer.Restart();
        }
    }
}

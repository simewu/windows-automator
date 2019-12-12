using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automator
{
    public partial class MouseLogger : Form
    {
        System.Windows.Forms.Timer timer;

        public MouseLogger()
        {
            InitializeComponent();
        }

        private void MouseLogger_Load(object sender, EventArgs e)
        {
        }

        public void step(object sender, EventArgs e)
        {
            string s = "";
            s = "Mouse: (" + Cursor.Position.X + ", " + Cursor.Position.Y + ")";
            Color c = Functions.GetColorAt(Cursor.Position.X, Cursor.Position.Y);
            s += "\tColor: (" + c.R + "," + c.G + "," + c.B + ")";
            list.Items.Add(s);
        }

        private void toggle_Click(object sender, EventArgs e)
        {
            if (timer == null)
            {
                list.Items.Clear();
                timer = new System.Windows.Forms.Timer();
                timer.Tick += new EventHandler(step);
                timer.Interval = Decimal.ToInt32(1000);
                timer.Start();
                toggle.Text = "Stop Listener";
            }
            else
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
                toggle.Text = "Start Listener";
            }
        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = list.SelectedItem.ToString();
            string[] tokens = s.Split(new char[] { ',', '(', ')' });
            Point p = new Point(int.Parse(tokens[1]), int.Parse(tokens[2]));
            Cursor.Position = p;
        }
    }
}

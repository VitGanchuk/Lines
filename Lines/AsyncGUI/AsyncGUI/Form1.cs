using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncGUI {
    public partial class Form1 : Form {

        private MyLabel label;

        public Form1() {
            InitializeComponent();
            label = new MyLabel();
            label.Text = "Huraaa!";
            label.Location = new Point(10, 50);
            panel1.Controls.Add(label);

        }

        private async void Form1_Click(object sender, EventArgs e) {
            for (int i = 0; i < 100; i++) {
                await label.MoveForward();
            }
        }

        private async void panel1_Click(object sender, EventArgs e) {
            for (int i = 0; i < 100; i++) {
                await label.MoveForward();
            }
        }
    }
}

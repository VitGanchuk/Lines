using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeSolver {
    public partial class Square : UserControl {

        public EventHandler StartRequested;

        private const int _SIZE = 40;

        private SquareMode _mode;
        public SquareMode Mode {
            get { return _mode; }
            set {
                _mode = value;
                switch (_mode) {
                    case SquareMode.Empty:
                        BackColor = Color.LightGray;
                        break;
                    case SquareMode.Start:
                        BackColor = Color.LightGreen;
                        break;
                    case SquareMode.Finish:
                        BackColor = Color.LimeGreen;
                        break;
                    case SquareMode.Path:
                        BackColor = Color.LightBlue;
                        break;
                    case SquareMode.Frontier:
                        BackColor = Color.Tomato;
                        break;
                    case SquareMode.Obstacle:
                        BackColor = Color.SaddleBrown;
                        break;
                    case SquareMode.Visited:
                        BackColor = Color.DimGray;
                        break;
                }
                this.Invalidate();
                this.Update();
                this.Refresh();
            }
        }

        public int Weight {
            get { return Int32.Parse(lbWeight.Text); }
            set {
                lbWeight.Text = value.ToString();
                lbWeight.Invalidate();
                lbWeight.Update();
                lbWeight.Refresh();
            }
        }

        private bool _isVisited;
        public bool IsVisited {
            get { return Mode == SquareMode.Obstacle ? true : _isVisited; }
            set { _isVisited = value; }
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public Square() {
            InitializeComponent();
            IsVisited = false;         
            Height = _SIZE;
            Width = Height;
            Mode = SquareMode.Empty;
            foreach (Control control in Controls) {
                control.MouseClick += OnMouseClick;
            }
        }

        public static int GetSize() {
            return _SIZE;
        }

        private void OnMouseClick(object sender, MouseEventArgs e) {
            switch(e.Button) {
                case MouseButtons.Left:
                    if((Control.ModifierKeys & Keys.Control) == Keys.Control) {
                        Mode = SquareMode.Obstacle;
                    } else if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) {
                        Mode = SquareMode.Start;
                    } else if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt) {
                        Mode = SquareMode.Finish;
                    } 
                    break;
                case MouseButtons.Middle:
                    StartRequested?.Invoke(this, EventArgs.Empty);
                    break;
                case MouseButtons.Right:
                    Mode = SquareMode.Empty;
                    break;
            }
        }

        public override string ToString() {
            return String.Format("Row: {0} | Column: {1} | Mode: {2}", Row, Column, Enum.GetName(typeof(SquareMode), Mode));
        }
    }

    public enum SquareMode { Empty, Current, Path, Start, Finish, Frontier, Visited, Obstacle }
}

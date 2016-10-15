using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lines
{
    public enum Colors { Aqua, Blue, Green, Grey, LightBlue, Orange, Pink, Red, Yellow };
    public enum BallSize { Initial, HalfSqueezed, FullSqueezed };
    public enum BallDirection { Up, Down, Stop };

    class Ball : UserControl
    {
        #region Events

        public event EventHandler BallSelected;

        #endregion

        #region Fields and properties

        private PictureBox pbBall;
        private System.Windows.Forms.Timer _timer;
        private BallDirection _direction;

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                if (_isSelected)
                {
                    BallSelected?.Invoke(this, EventArgs.Empty);
                    StartJump();
                }
                else
                {
                    StopJump();
                }
            }
        }

        private Colors _ballColor;
        public Colors BallColor
        {
            get { return _ballColor; }
            set
            {
                _ballColor = value;
                switch (value)
                {
                    case Colors.Aqua:
                        pbBall.Image = Properties.Resources.Lines_aqua;
                        break;
                    case Colors.Blue:
                        pbBall.Image = Properties.Resources.Lines_blue;
                        break;
                    case Colors.Green:
                        pbBall.Image = Properties.Resources.Lines_green;
                        break;
                    case Colors.Grey:
                        pbBall.Image = Properties.Resources.Lines_grey;
                        break;
                    case Colors.LightBlue:
                        pbBall.Image = Properties.Resources.Lines_lightBlue;
                        break;
                    case Colors.Orange:
                        pbBall.Image = Properties.Resources.Lines_orange;
                        break;
                    case Colors.Pink:
                        pbBall.Image = Properties.Resources.Lines_pink;
                        break;
                    case Colors.Red:
                        pbBall.Image = Properties.Resources.Lines_red;
                        break;
                    case Colors.Yellow:
                        pbBall.Image = Properties.Resources.Lines_yellow;
                        break;
                }

            }
        }

        private BallSize __size;
        private BallSize _size
        {
            get { return __size; }
            set
            {
                __size = value;
                switch (value)
                {
                    case BallSize.Initial:
                        pbBall.Height = 41;
                        break;
                    case BallSize.HalfSqueezed:
                        pbBall.Height = 39;
                        break;
                    case BallSize.FullSqueezed:
                        pbBall.Height = 37;
                        break;
                }
            }
        }

        #endregion

        #region Constructors

        private void InitializeComponent()
        {
            this.pbBall = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBall)).BeginInit();
            this.SuspendLayout();
            // 
            // pbBall
            // 
            this.pbBall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbBall.Image = global::Lines.Properties.Resources.Lines_aqua;
            this.pbBall.Location = new System.Drawing.Point(0, 0);
            this.pbBall.Name = "pbBall";
            this.pbBall.Size = new System.Drawing.Size(41, 41);
            this.pbBall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBall.TabIndex = 0;
            this.pbBall.TabStop = false;
            this.pbBall.Click += new System.EventHandler(this.OnClick);
            // 
            // ucBall
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.CausesValidation = false;
            this.Controls.Add(this.pbBall);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "ucBall";
            this.Size = new System.Drawing.Size(41, 41);
            ((System.ComponentModel.ISupportInitialize)(this.pbBall)).EndInit();
            this.ResumeLayout(false);

        }

        public Ball()
        {
            InitializeComponent();
            __size = BallSize.Initial;
            _direction = BallDirection.Stop;
        }

        #endregion

        #region Methods

        public void MoveTo(Point destination)
        {
            Thread.Sleep(100);
            Location = destination;
        }
        
        private void OnJumpTimerTick(object sender, EventArgs e)
        {
            if (_direction == BallDirection.Up)
            {
                if (pbBall.Location.Y == -2)
                {
                    _size = _size == BallSize.Initial ? BallSize.HalfSqueezed : (_size == BallSize.HalfSqueezed ? BallSize.FullSqueezed : BallSize.FullSqueezed);
                    _direction = _size == BallSize.FullSqueezed ? BallDirection.Down : BallDirection.Up;
                }
                else
                {
                    pbBall.Location = _size == BallSize.Initial ? new Point(pbBall.Location.X, pbBall.Location.Y - 1) : new Point(pbBall.Location.X, pbBall.Location.Y - 2);
                    _size = _size == BallSize.FullSqueezed ? BallSize.HalfSqueezed : (_size == BallSize.HalfSqueezed ? BallSize.Initial : BallSize.Initial);
                }
            }
            else if(_direction == BallDirection.Down)
            {
                if (pbBall.Location.Y == 2 || pbBall.Location.Y == 4 || pbBall.Location.Y == 6)
                {
                    _size = _size == BallSize.Initial ? BallSize.HalfSqueezed : (_size == BallSize.HalfSqueezed ? BallSize.FullSqueezed : BallSize.FullSqueezed);
                    pbBall.Location = _size == BallSize.HalfSqueezed || _size == BallSize.FullSqueezed ? new Point(pbBall.Location.X, pbBall.Location.Y + 2) : pbBall.Location;
                    _direction = _size == BallSize.FullSqueezed ? BallDirection.Up : BallDirection.Down;
                }
                else
                {
                    _size = _size == BallSize.FullSqueezed ? BallSize.HalfSqueezed : (_size == BallSize.HalfSqueezed ? BallSize.Initial : BallSize.Initial);
                    pbBall.Location = _size == BallSize.Initial ? new Point(pbBall.Location.X, pbBall.Location.Y + 1) : pbBall.Location;
                }
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            IsSelected = !IsSelected;
        }

        private void StartJump()
        {
            _direction = BallDirection.Up;
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 40;
            _timer.Tick += OnJumpTimerTick;
            _timer.Start();
        }

        private void StopJump()
        {
            pbBall.Location = new Point(pbBall.Location.X, pbBall.Location.X);
            _size = BallSize.Initial;
            _timer.Stop();
        }

        #endregion
    }
}

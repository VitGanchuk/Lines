using Lines.Properties;
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

namespace Lines
{
    public partial class fMain : Form
    {
        #region Fields and properties

        private const int SIZE = 9;

        private Square[,] _squares;
        private List<Ball> _balls;

        #endregion

        #region Constructors

        public fMain()
        {
            InitializeComponent();
            _squares = new Square[SIZE, SIZE];
            _balls = new List<Ball>();
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    _squares[i, j] = new Square() { Location = new Point(2 + 45 * j, 2 + 45 * i), Ball = null, Row = i, Column = j };
                }
            }
            CreateBalls(3);
        }

        #endregion

        #region Methods

        private void CreateBalls(int count)
        {
            Ball ball = null;
            Random rand = new Random();
            if(_balls.Count + count > SIZE * SIZE)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                ball = new Ball();
                ball.BallColor = (Colors)rand.Next(SIZE);
                ball.BallSelected += OnBallSelected;
                int x = 0;
                int y = 0;
                do
                {
                    x = rand.Next(SIZE);
                    y = rand.Next(SIZE);
                } while (_squares[x, y].Ball != null);

                _squares[x, y].Ball = ball;
                pbField.Controls.Add(ball);
                _balls.Add(ball);
            }
        }

        private void OnBallSelected(object sender, EventArgs e)
        {
            Ball selected = sender as Ball;
            if (selected != null)
            {
                foreach (Ball ball in _balls.Where(b => b.IsSelected && b != selected))
                {
                    ball.IsSelected = false;
                }
            }
        }

        private Square GetSquare(int x, int y)
        {
            return _squares[y / 45, x / 45];
        }
        #endregion

        private void pbField_MouseClick(object sender, MouseEventArgs e)
        {
            //Square square = GetSquare(e.X, e.Y);
            //MessageBox.Show(square.Row + " " + square.Column);
            Ball selectedBall = _balls.FirstOrDefault(b => b.IsSelected);
            if(selectedBall != null)
            {
                Square start = GetSquare(selectedBall.Location.X, selectedBall.Location.Y);
                start.Ball = null;

                Square destination = start; 

                for (int i = 1; i < 4; i++)
                {
                    destination = _squares[start.Row, start.Column + i];
                    selectedBall.MoveTo(destination.Location);
                }

                destination.Ball = selectedBall;
            }
        }
    }
}

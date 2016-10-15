using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines
{
    class Square
    {
        #region Events

        #endregion

        #region Fields and properties

        public Point Location { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        private Ball _ball;
        public Ball Ball
        {
            get { return _ball; }
            set
            {
                _ball = value;
                if(_ball != null)
                {
                    _ball.Location = new Point(Location.X, Location.Y);
                } 
            }
        }

        #endregion

        #region Constructors

        public Square()
        {
            
        }

        #endregion

        #region Methods

        #endregion
    }
}

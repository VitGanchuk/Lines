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

namespace MazeSolver {
    public partial class Form1 : Form {

        public delegate void SolveMazeDelegate();

        private const int _SIZE = 17;
        private Square[,] _field;
        private List<Square> _squares;

        public Form1() {
            InitializeComponent();
            _field = new Square[_SIZE, _SIZE];
            _squares = new List<Square>();
            for (int i = 0; i < _SIZE; i++) {
                for (int j = 0; j < _SIZE; j++) {
                    Square square = new Square();
                    square.Location = new Point(j * Square.GetSize() + 2, i * Square.GetSize() + 2);
                    square.Row = i;
                    square.Column = j;
                    square.StartRequested += OnStartRequested;
                    _field[i, j] = square;
                    _squares.Add(square);
                    pnField.Controls.Add(square);
                }
            }
        }

        private void OnStartRequested(object sender, EventArgs e) {
            bool forward = true;
            Random random = new Random();
            Square start = _squares.FirstOrDefault(i => i.Mode == SquareMode.Start);
            Square finish = _squares.FirstOrDefault(i => i.Mode == SquareMode.Finish);
            if(start == null || finish == null) {
                MessageBox.Show("Whether the start or finish point (or both) doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < _SIZE; i++) {
                for (int j = 0; j < _SIZE; j++) {
                    _field[i, j].Weight = Math.Abs(_field[i, j].Row - finish.Row) + Math.Abs(_field[i, j].Column - finish.Column);
                    if (_field[i, j].Mode != SquareMode.Start && _field[i, j].Mode != SquareMode.Finish && _field[i, j].Mode != SquareMode.Obstacle) {
                        _field[i, j].Mode = SquareMode.Empty;
                    }
                }
            }
            Stack<Square> path = new Stack<Square>();
            path.Push(start);
            Square current = start;
            while (current != finish) {
                List<Square> neighbours = Neighbours(current, forward);
                if (neighbours != null) {
                    int min = neighbours.Min(i => i.Weight);
                    neighbours = neighbours.Where(i => i.Weight == min).ToList();
                    if (neighbours.Count > 1) {
                        if(neighbours.Any(i => i.Mode == SquareMode.Empty)) {
                            if(neighbours.Where(i => i.Mode == SquareMode.Empty).Count() > 1) {
                                current = neighbours[random.Next(neighbours.Where(i => i.Mode == SquareMode.Empty).Count())];
                            } else {
                                current = neighbours.Where(i => i.Mode == SquareMode.Empty).First();
                            }
                        }
                        else {
                            current = neighbours[random.Next(neighbours.Count)];
                        }
                    } else {
                        current = neighbours.First(i => i.Weight == min);
                    }
                    if (current == start) {
                        while (path.Peek() != start) {
                            path.Pop().Mode = SquareMode.Visited;
                        }
                    } else {
                        current.Mode = current == finish ? SquareMode.Finish : SquareMode.Current;
                        path.Peek().Mode = path.Peek() == start ? SquareMode.Start : SquareMode.Path;
                        path.Push(current);
                        ClearPath(path);
                    }
                    forward = true;
                } else {
                    if (current == start) { break; }
                    //if (path.Count > 1) path.Pop().Mode = SquareMode.DeadEnd;
                    current = path.Peek();
                    current.Mode = current == start ? SquareMode.Start : SquareMode.Current;
                    forward = false;
                }
                Thread.Sleep(100);
            }
        }

        private List<Square>Neighbours(Square current, bool forward) {
            List<Square> neighbours = new List<Square>();
            Square neighbour = null;
            foreach (Tuple<int, int> index in NeighbourIndecies(current)) {
                neighbour = _field[index.Item1, index.Item2];
                if (neighbour.Mode == SquareMode.Empty || (neighbour.Mode == SquareMode.Visited && !forward) || neighbour.Mode == SquareMode.Finish) {
                    neighbours.Add(neighbour);
                }
            }
            return neighbours.Count > 0 ? neighbours : null;
        }

        private void ClearPath(Stack<Square> path) {
            if(path.Count < 4) { return; }
            Square current = path.Peek();
            Square neighbour = null;
            Square square = null;
            foreach (Tuple<int, int> index in NeighbourIndecies(current)) {
                neighbour = _field[index.Item1, index.Item2];
                if ((neighbour.Mode == SquareMode.Path || neighbour.Mode == SquareMode.Start) && neighbour != path.ElementAt(1)) {
                    while (path.Peek() != neighbour) {
                        square = path.Pop();
                        square.Mode = SquareMode.Visited;
                    }
                    current.Mode = SquareMode.Current;
                    path.Push(current);
                }
            }
        }

        private List<Tuple<int,int>> NeighbourIndecies(Square current) {
            List<Tuple<int, int>> indecies = new List<Tuple<int, int>>();
            if (current.Row - 1 >= 0) {
                indecies.Add(new Tuple<int, int>(current.Row - 1, current.Column));
            }
            if (current.Column - 1 >= 0) {
                indecies.Add(new Tuple<int, int>(current.Row, current.Column - 1));
            }
            if (current.Column + 1 < _SIZE) {
                indecies.Add(new Tuple<int, int>(current.Row, current.Column + 1));
            }
            if (current.Row + 1 < _SIZE) {
                indecies.Add(new Tuple<int, int>(current.Row + 1, current.Column));
            }
            return indecies;
        }
    }
}

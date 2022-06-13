using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Food
    {
        private Point _position;

        public Point Position
        {
            get => _position;
            set => _position = value;
        }
        public Food(int row, int col)
        {
            _position = new Point(row, col);
        }

        public Food(Point position)
        {
            _position = position;
        }
    }
}

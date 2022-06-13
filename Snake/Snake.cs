using System.Security.Cryptography.X509Certificates;

namespace Snake;
public enum Direction
{
    ToTop, ToBottom, ToLeft, ToRight
}
public class Snake
{
    private List<Point> _coords = new ();
    private Random _rand;
    private int _length = 3;
    private bool _chDir = false;
    private bool _grow = false;

    public List<Point> Coords => new (_coords);
    private Point Head
    {
        get => _coords[0];
        set => _coords[0] = value;
    }
    private Point Tail
    {
        get => _coords[^1]; // ^1 - индекс от конца списка
        set => _coords[^1] = value;
    }
    private Point PreTail => _coords[^2];
    public int Length => _length;

    public Direction CurrentDirection
    {
        get;
        private set;
    }
    public Snake(Point startPoint)
    {
        _rand = new Random((int)DateTime.Now.Ticks);
        CurrentDirection = (Direction)_rand.Next(4);
        _coords.Add(startPoint);
        int x = -1, y = -1;
        switch (CurrentDirection)
        {
            case Direction.ToBottom:
            {
                x = _coords[0].X;
                y = _coords[0].Y - (Length - 1);
                break;
            }
            case Direction.ToTop:
            {
                x = _coords[0].X;
                y = _coords[0].Y + Length - 1;
                break;
            }
            case Direction.ToLeft:
            {
                x = _coords[0].X + Length - 1;
                y = _coords[0].Y;
                break;
            }
            case Direction.ToRight:
            {
                x = _coords[0].X - (Length - 1);
                y = _coords[0].Y;
                break;
            }
        }
        _coords.Add(new Point(x, y));
    }

    public void Turn(Direction dir)
    {
        if (CurrentDirection != dir)
        {
            CurrentDirection = dir;
            _chDir = true;
        }
    }

    public void Crawl()
    {
        if (_chDir)
        {
            int x = -1, y = -1;
            switch (CurrentDirection)
            {
                case Direction.ToBottom:
                {
                    x = _coords[0].X;
                    y = _coords[0].Y + 1;
                    break;
                }
                case Direction.ToTop:
                {
                    x = _coords[0].X;
                    y = _coords[0].Y - 1;
                        break;
                }
                case Direction.ToLeft:
                {
                    x = _coords[0].X - 1;
                    y = _coords[0].Y;
                        break;
                }
                case Direction.ToRight:
                {
                    x = _coords[0].X + 1;
                    y = _coords[0].Y;
                        break;
                }
            }
            _coords.Insert(0, new Point(x, y));
            _chDir = false;
        }
        else
        {
            switch (CurrentDirection)
            {
                case Direction.ToBottom:
                {
                    Head = new Point(Head.X, Head.Y + 1);
                    break;
                }
                case Direction.ToTop:
                {
                    Head = new Point(Head.X, Head.Y - 1);
                    break;
                }
                case Direction.ToLeft:
                {
                    Head = new Point(Head.X - 1, Head.Y);
                    break;
                }
                case Direction.ToRight:
                {
                    Head = new Point(Head.X + 1, Head.Y);
                    break;
                }
            }
        }

        if (!_grow)
        {
            switch (GetTailDirection())
            {
                case Direction.ToTop:
                {
                    Tail = new Point(Tail.X, Tail.Y - 1);
                    break;
                }
                case Direction.ToBottom:
                {
                    Tail = new Point(Tail.X, Tail.Y + 1);
                    break;
                }
                case Direction.ToLeft:
                {
                    Tail = new Point(Tail.X - 1, Tail.Y);
                    break;
                }
                case Direction.ToRight:
                {
                    Tail = new Point(Tail.X + 1, Tail.Y);
                    break;
                }
            }

            if (Tail == PreTail)
            {
                _coords.Remove(Tail);
            }
        }
        else
        {
            _grow = false;
        }
    }

    private Direction GetTailDirection()
    {
        if (PreTail.X < Tail.X) return Direction.ToLeft;
        if (PreTail.X > Tail.X) return Direction.ToRight;
        if (PreTail.Y < Tail.Y) return Direction.ToTop;
        return Direction.ToBottom;
    }

    public void Grow()
    {
        _grow = true;
    }
    public bool Ate(Food f) => _coords[0].X == f.Position.X && _coords[0].Y == f.Position.Y;

    public bool Contains(Point point)
    {
        for (int i = 0; i < _coords.Count - 1; i++)
        {
            var c1 = _coords[i];
            var c2 = _coords[i + 1];
            if (c1.X == c2.X)
            {
                var y1 = Math.Min(_coords[i].Y, _coords[i + 1].Y);
                var y2 = Math.Max(_coords[i].Y, _coords[i + 1].Y);
                while (y1 <= y2)
                {
                    if (c1.X == point.X && y1 == point.Y) return true;
                    y1++;
                }
            }
            else
            {
                var x1 = Math.Min(_coords[i].X, _coords[i + 1].X);
                var x2 = Math.Max(_coords[i].X, _coords[i + 1].X);
                while (x1 <= x2)
                {
                    if (c1.Y == point.Y && x1 == point.X) return true;
                    x1++;
                }
            }
        }
        return false;
    }
}

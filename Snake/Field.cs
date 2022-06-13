using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake;
public class Field
{
    private Random _rand;
    private int _rows;
    private int _cols;
    private Snake _snake;
    private Food _food;

    public List<Point> SnakePosition => _snake.Coords;
    public Point FoodPosition => _food.Position;

    public Direction SnakeDirection
    {
        get => _snake.CurrentDirection;
        set
        {
            _snake.Turn(value);
        }
    }
    public int Rows
    {
        get => _rows;
        set
        {
            if (value < 10) value = 10;
            if (value > 100) value = 100;
            _rows = value;
        }
    }

    public int Columns
    {
        get => _cols;
        set
        {
            if (value < 10) value = 10;
            if (value > 100) value = 100;
            _cols = value;
        }
    }

    public Field() : this(30, 30)
    {
    }

    public Field(int rows, int cols)
    {
        Rows = rows;
        Columns = cols;
        _rand = new Random((int)DateTime.Now.Ticks);
        _snake = new Snake(new Point(_rand.Next(3, Rows - 3), _rand.Next(3, Columns - 3)));
        NewFood();
    }

    private void NewFood()
    {
        do
        {
            _food = new Food(_rand.Next(3, Rows - 3), _rand.Next(3, Columns - 3));
        } while (_snake.Contains(_food.Position));
    }
    public void NextStep()
    {
        _snake.Crawl();
        if (_snake.Ate(_food))
        {
            _snake.Grow();
            NewFood();
        }
    }
}
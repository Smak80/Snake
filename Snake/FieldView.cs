using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class FieldView
    {
        private Field _field;
        private Graphics _mainG;
        private Thread? t;
        public int CellSize
        {
            get
            {
                var w = _mainG.VisibleClipBounds.Width;
                var h = _mainG.VisibleClipBounds.Height;
                var cellW = (int)(w / _field.Columns);
                var cellH = (int)(h / _field.Rows);
                return Math.Min(cellW, cellH);
            }
        }

        private Rectangle MainRectangle
        {
            get
            {
                var w = _mainG.VisibleClipBounds.Width;
                var h = _mainG.VisibleClipBounds.Height;
                var r = new Rectangle();
                r.Width = CellSize * _field.Columns;
                r.Height = CellSize * _field.Rows;
                r.X = (int)((w - r.Width) / 2);
                r.Y = (int)((h - r.Height) / 2);
                return r;
            }
        }

        public FieldView(Field f)
        {
            _field = f;
        }

        public void StartOn(Graphics g)
        {
            _mainG = g;
            if (t == null || !t.IsAlive)
            {
                t = new Thread(Animate);
                t.IsBackground = true;
                t.Start();
            }
        }

        private void Paint(Graphics g)
        {
            g.Clear(Color.DarkGray);
            DrawGrid(g);
            DrawSnake(g);
            DrawFood(g);
        }

        private void DrawFood(Graphics g)
        {
            //throw new NotImplementedException();
        }

        private void DrawSnake(Graphics g)
        {
            var pos = new List<Point>(_field.SnakePosition);
            var first = true;
            for (int i = 0; i < pos.Count - 1; i++)
            {
                var x1 = pos[i].X;
                var y1 = pos[i].Y;
                var x2 = pos[i + 1].X;
                var y2 = pos[i + 1].Y;
                if (x1 == x2)
                {
                    var sh = y1 < y2 ? y1 : y2;
                    var ln = Math.Abs(y1 - y2);
                    var c = 0;
                    while (c <= ln)
                    {
                        first = y1 < y2 && c == 0 ||
                                y1 > y2 && c == ln;
                        var cs = first ? CellState.Head : CellState.Body;
                        
                        var r = new Rectangle(
                            MainRectangle.Left + x1 * CellSize,
                            MainRectangle.Top + (c + sh) * CellSize,
                            CellSize, CellSize
                            );
                        PaintCell(g, r, cs);
                        c++;
                    }
                }
                else
                {
                    var sh = x1 < x2 ? x1 : x2;
                    var ln = Math.Abs(x1 - x2);
                    var c = 0;
                    while (c <= ln)
                    {
                        first = y1 < y2 && c == 0 ||
                                y1 > y2 && c == ln;
                        var cs = first ? CellState.Head : CellState.Body;
                        var r = new Rectangle(
                            MainRectangle.Left + (c + sh) * CellSize,
                            MainRectangle.Top + y1 * CellSize,
                            CellSize, CellSize
                        );
                        PaintCell(g, r, cs);
                        c++;
                    }
                }
            }
        }

        private void PaintCell(Graphics g, Rectangle r, CellState cs)
        {
            CellView.Paint(g, r, cs);
        }

        private void DrawGrid(Graphics g)
        {
            for (int i = 0; i < _field.Rows; i++)
            {
                for (int j = 0; j < _field.Columns; j++)
                {
                    var r = new Rectangle(
                        MainRectangle.Left + CellSize * j, 
                        MainRectangle.Top + CellSize * i, 
                        CellSize, 
                        CellSize);
                    CellView.Paint(g, r, CellState.Empty);
                }
            }
        }

        private void Animate()
        {
            try
            {
                while (true)
                {
                    BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(
                        _mainG,
                        Rectangle.Ceiling(_mainG.VisibleClipBounds)
                    );
                    Paint(bg.Graphics);
                    bg.Render(_mainG);
                    bg.Dispose();
                    Thread.Sleep(30);
                }
            }
            finally
            {
                
            }
        }
    }
}

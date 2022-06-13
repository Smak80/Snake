using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum CellState
    {
        Empty, Head, Body, Food
    }
    public static class CellView
    {
        public static void Paint(Graphics g, Rectangle r, CellState state)
        {
            var p = new Pen(Color.SandyBrown);
            g.DrawRectangle(p, r);
            switch (state)
            {
                case CellState.Empty:
                {
                    var eb = new SolidBrush(Color.DarkBlue);
                    g.FillRectangle(eb, r.X+1, r.Y+1, r.Width-2, r.Height-2);
                    break;
                }
                case CellState.Head:
                {
                    var sp = new Pen(Color.Green);
                    var stp = new Pen(Color.Red, 3);
                    var sb = new SolidBrush(Color.ForestGreen);
                    g.DrawEllipse(sp, r.X+2, r.Y+5, r.Width-4, r.Height-7);
                    g.FillEllipse(sb, r.X + 2, r.Y + 2, r.Width - 4, r.Height - 4);
                    g.DrawLine(stp, r.X+1+r.Width/2, r.Y+1, r.X+1+r.Width/2, r.Y+5);
                    break;
                }
                case CellState.Body:
                {
                    var sp = new Pen(Color.Green);
                    var sb = new SolidBrush(Color.ForestGreen);
                    g.DrawEllipse(sp, r.X + 2, r.Y + 2, r.Width - 4, r.Height - 4);
                    g.FillEllipse(sb, r.X + 2, r.Y + 2, r.Width - 4, r.Height - 4);
                    break;
                }
                case CellState.Food:
                {
                    var sp = new Pen(Color.Blue);
                    var sb = new SolidBrush(Color.BlueViolet);
                    g.DrawEllipse(sp, r.X + 2, r.Y + 2, r.Width - 4, r.Height - 4);
                    g.FillEllipse(sb, r.X + 2, r.Y + 2, r.Width - 4, r.Height - 4);
                    break;
                }
            }
        }
    }
}

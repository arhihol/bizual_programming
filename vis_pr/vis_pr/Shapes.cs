using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace vis_pr
{
    public abstract class Shapes
    {
        public abstract void DrawWith(Graphics g, Pen p);
    }

    public class Cross : Shapes
    {
        Point XY = new Point();
        public Cross(Point _XY)
        {
            this.XY = _XY;
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawLine(p, XY.X - 3, XY.Y - 3, XY.X + 3, XY.Y + 3);
            g.DrawLine(p, XY.X + 3, XY.Y - 3, XY.X - 3, XY.Y + 3);
        }
    }

    class Line : Shapes
    {
        private Point A, B;

        public Line(Point _A, Point _B)
        {
            this.A = _A;
            this.B = _B;
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawLine(p, A.X, A.Y, B.X, B.Y);
        }
    }
}

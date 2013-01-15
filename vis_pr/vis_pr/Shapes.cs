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
        public abstract string DescriptionString { get; }
        public abstract void DrawWith(Graphics g, Pen p);
    }

    public class Cross : Shapes
    {
        Point XY = new Point();

        public override string DescriptionString
        {
            get
            {
                return "Cross(X=" + XY.X + ",Y=" + XY.Y + ")";
            }
        }

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

        public override string DescriptionString
        {
            get
            {
                return "Line(" + A + "; " + B + ")";
            }
        }

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

    class Circle : Shapes
    {
        private Point C, R;
        Pen p = new Pen(Color.Black);

        public override string DescriptionString
        {
            get
            {
                return "Circle(C=" + C + ",R=" + R + ")";
            }
        }

        public float Radius
        {
            get { return (float)Math.Sqrt(Math.Pow(R.X - C.X, 2) + Math.Pow(R.Y - C.Y, 2)); }
        }

        public Circle(Point _C, Point _point_R)
        {
            this.C = _C;
            this.R = _point_R;
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawEllipse(p, C.X - this.Radius, C.Y - this.Radius, Radius * 2, Radius * 2);

        }
    }
}

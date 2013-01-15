using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace vis_pr
{
    public abstract class Shapes
    {
        public abstract string DescriptionString { get; }
        public abstract void DrawWith(Graphics g, Pen p);
        public abstract void Save(StreamWriter sv);
    }

    public class Cross : Shapes
    {
        Point XY = new Point();

        public override string DescriptionString
        {
            get
            {
                return "Cross(X= " + XY.X + " ,Y= " + XY.Y + " )";
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

        public override void Save(StreamWriter sv)
        {
            sv.WriteLine("Cross");
            sv.WriteLine(Convert.ToString(XY.X) + " " + Convert.ToString(XY.Y));
        }

        public Cross(StreamReader sr)
        {
            String line = sr.ReadLine();
            string[] foo = line.Split(' ');
            XY.X = Convert.ToInt32(foo[0]);
            XY.Y = Convert.ToInt32(foo[1]);
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

        public override void Save(StreamWriter sv)
        {
            sv.WriteLine("Line");
            sv.WriteLine(Convert.ToString(A.X) + " " + Convert.ToString(A.Y));
            sv.WriteLine(Convert.ToString(B.X) + " " + Convert.ToString(B.Y));
        }

        public Line(StreamReader sr)
        {
            string line = sr.ReadLine();
            string[] foo = line.Split(' ');
            A.X = Convert.ToInt32(foo[0]);
            A.Y = Convert.ToInt32(foo[1]);

            line = sr.ReadLine();
            foo = line.Split(' ');
            B.X = Convert.ToInt32(foo[0]);
            B.Y = Convert.ToInt32(foo[1]);
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

        public override void Save(StreamWriter sv)
        {
            sv.WriteLine("Circle");
            sv.WriteLine(Convert.ToString(C.X) + " " + Convert.ToString(C.Y));
            sv.WriteLine(Convert.ToString(R.X) + " " + Convert.ToString(R.Y));
        }

        public Circle(StreamReader sr)
        {
            String circle = sr.ReadLine();
            string[] foo = circle.Split(' ');
            C.X = Convert.ToInt32(foo[0]);
            C.Y = Convert.ToInt32(foo[1]);

            circle = sr.ReadLine();
            foo = circle.Split(' ');
            R.X = Convert.ToInt32(foo[0]);
            R.Y = Convert.ToInt32(foo[1]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace vis_pr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Shapes> Shapes = new List<Shapes>();
        Shapes tempShape;
        Pen pMain = new Pen(Color.Black);
        Point xy = new Point();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                addShape(tempShape);
                Refresh();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shapes p in this.Shapes)
            {
                p.DrawWith(e.Graphics, pMain);
            }
        }

        private void addShape(Shapes shape)
        {
            Shapes.Add(shape);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                xy.X = e.X;
                xy.Y = e.Y;
                tempShape = new Cross(xy);
            }
        }
    }
}

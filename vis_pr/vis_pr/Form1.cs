﻿using System;
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
        Pen pTemp = new Pen(Color.Gray);
        Point xy_line_1 = new Point();
        Point xy_line_2 = new Point();
        Boolean flg_fig = false;
        Point Cen_okr = new Point();
        Point xy_okr = new Point();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                addShape(tempShape);
                Refresh();
            }
            if (radioButton2.Checked)
            {
                if (!flg_fig)
                {
                    flg_fig = true;
                    xy_line_1.X = e.X;
                    xy_line_1.Y = e.Y;
                    addShape(tempShape);
                    Refresh();
                }
                else
                {
                    flg_fig = false;
                    xy_line_1.X = xy_line_2.X;
                    xy_line_1.Y = xy_line_2.Y;
                    addShape(tempShape);
                    Refresh();
                }
            }
            if (radioButton3.Checked)
            {
                if (!flg_fig)
                {
                    flg_fig = true;
                    Cen_okr.X = e.X;
                    Cen_okr.Y = e.Y;
                    addShape(tempShape);
                    Refresh();
                }
                else
                {
                    flg_fig = false;
                    Cen_okr.X = xy_okr.X;
                    Cen_okr.Y = xy_okr.Y;
                    addShape(tempShape);
                    Refresh();
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (tempShape != null)
            {
                tempShape.DrawWith(e.Graphics, pTemp);
            }
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
            if (radioButton2.Checked)
            {
                if (flg_fig)
                {
                    xy_line_2.X = e.X;
                    xy_line_2.Y = e.Y;
                    tempShape = new Line(xy_line_1, xy_line_2);
                    Refresh();
                }
                else
                {
                    xy_line_1.X = xy_line_2.X;
                    xy_line_1.Y = xy_line_2.Y;
                    tempShape = new Line(xy_line_1, xy_line_2);
                }

            }
            if (radioButton3.Checked)
            {
                if (flg_fig)
                {
                    xy_okr.X = e.X;
                    xy_okr.Y = e.Y;
                    tempShape = new Circle(Cen_okr, xy_okr);
                    Refresh();
                }
                else
                {
                    Cen_okr.X = xy_okr.X;
                    Cen_okr.Y = xy_okr.Y;
                    tempShape = new Circle(Cen_okr, xy_okr);
                }
            }
        }
    }
}

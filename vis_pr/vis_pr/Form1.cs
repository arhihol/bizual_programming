using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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

        Pen p_osn = new Pen(Color.Black);
        Pen p_ris = new Pen(Color.Gray);
        Pen p_sel = new Pen(Color.Red);

        Point xy = new Point();

        Point xy_line_1 = new Point();
        Point xy_line_2 = new Point();

        Boolean flg_fig = false;

        Point Cen_okr = new Point();
        Point xy_okr = new Point();

        //Point xy_sel = new Point();
        //Boolean flg_sel = false;
        //string[] st;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                //flg_sel = false;
                addShape(tempShape);
                Refresh();
            }
            if (radioButton2.Checked)
            {
                //flg_sel = false;
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
                //flg_sel = false;
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
            //if (radioButton4.Checked)
            //{
            //    if (!flg_sel)
            //    {
            //        flg_sel = true;
            //        xy_sel.X = e.X;
            //        xy_sel.Y = e.Y;
            //        String ss = Convert.ToString(xy_sel.X) + " " + Convert.ToString(xy_sel.Y);
            //        textBox1.Text = ss;
            //    }
            //}
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (tempShape != null)
            {
                tempShape.DrawWith(e.Graphics, p_ris);
            }
            foreach (Shapes p in this.Shapes)
            {
                p.DrawWith(e.Graphics, p_osn);                               
            }
            //if (flg_sel)
            //{
            //    foreach (Shapes t in this.Shapes)
            //    {
            //        t.DrawWith(e.Graphics, p_osn);
            //        st = t.DescriptionString.Split(' ');
            //        if ((Convert.ToInt32(st[1]) + 5 > xy_sel.X && Convert.ToInt32(st[1]) - 5 < xy_sel.X) && (Convert.ToInt32(st[3]) + 5 > xy_sel.Y && Convert.ToInt32(st[3]) - 5 < xy_sel.Y))
            //        {
            //            MessageBox.Show("true");
            //            flg_sel = false;
            //        }
            //    }
            //}
            foreach (int i in shapesList.SelectedIndices)
            {
                Shapes[i].DrawWith(e.Graphics, p_sel);
            }
        }

        private void addShape(Shapes shape)
        {
            Shapes.Add(shape);
            shapesList.Items.Add(shape.DescriptionString);
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
            //if (radioButton4.Checked)
            //{
            //    if (!flg_sel)
            //    {
            //        flg_sel = true;
            //        xy_sel.X = e.X;
            //        xy_sel.Y = e.Y;
            //        String ss = Convert.ToString(xy_sel.X) + " " + Convert.ToString(xy_sel.Y);
            //        textBox1.Text = ss;
            //    }
            //}
        }

        private void shapesList_SelectedValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void shapesList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                if (shapesList.SelectedIndices.Count > 0)
                {
                    Shapes.RemoveAt(shapesList.SelectedIndices[0]);
                    shapesList.Items.RemoveAt(shapesList.SelectedIndices[0]);
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name_file = "test.txt";
            StreamWriter sv = new StreamWriter(name_file);
            foreach (Shapes p in this.Shapes)
            {
                p.Save(sv);
            }
            sv.Close();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name_file = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                name_file = openFileDialog1.FileName;
            }
            StreamReader sr = new StreamReader(name_file);

            while (!sr.EndOfStream)
            {
                string type = sr.ReadLine();
                switch (type)
                {
                    case "Cross":
                        Shapes.Add(new Cross(sr));
                        break;
                    case "Line":
                        Shapes.Add(new Line(sr));
                        break;
                    case "Circle":
                        Shapes.Add(new Circle(sr));
                        break;
                }
            }
            sr.Close();
            Refresh();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shapes.Clear();
            shapesList.ClearSelected();
            shapesList.Items.Clear();
            Refresh();
        }
    }
}

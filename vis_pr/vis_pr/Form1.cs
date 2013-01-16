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

        Point xy_line_1 = new Point();
        Point xy_line_2 = new Point();

        Boolean flg_fig = false;
        Boolean flg_sel = false;

        Point Cen_okr = new Point();
        Point xy_okr = new Point();

        Point xy_sel_old = new Point();
        Point xy_sel_new = new Point();

        Graphics gr;

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
                    xy_line_1 = e.Location;
                }
                else
                {
                    flg_fig = false;
                    xy_line_1 = xy_line_2;
                    addShape(tempShape);
                    Refresh();
                }
            }
            if (radioButton3.Checked)
            {
                if (!flg_fig)
                {
                    flg_fig = true;
                    Cen_okr = e.Location;
                }
                else
                {
                    flg_fig = false;
                    Cen_okr = xy_okr;
                    addShape(tempShape);
                    Refresh();
                }
            }
            if (radioButton4.Checked)
            {
                if (flg_sel)
                {
                    flg_sel = true;
                    xy_sel_old = e.Location;
                }
                else
                {
                    flg_sel = false;
                    xy_sel_old = xy_sel_new;
                    foreach (Shapes t in this.Shapes)
                    {
                        if (t.IsNearTo(xy_sel_old))
                        {
                            t.DrawWith(gr, p_sel);
                        }
                    }
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
                if ((tempShape != null) && (!radioButton4.Checked))
                {
                    tempShape.DrawWith(e.Graphics, p_ris);
                }
                foreach (Shapes p in this.Shapes)
                {
                    p.DrawWith(e.Graphics, p_osn);
                }
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
                tempShape = new Cross(e.Location);
            }
            if (radioButton2.Checked)
            {
                if (flg_fig)
                {
                    tempShape = new Line(xy_line_1, e.Location);
                    Refresh();
                }
                else
                {
                    xy_line_1 = xy_line_2;
                    tempShape = new Line(xy_line_1, e.Location);
                }
            }
            if (radioButton3.Checked)
            {
                if (flg_fig)
                {
                    tempShape = new Circle(Cen_okr, e.Location);
                    Refresh();
                }
                else
                {
                    Cen_okr = xy_okr;
                    tempShape = new Circle(Cen_okr, e.Location);
                }
            }
            if (radioButton4.Checked)
            {
                xy_sel_new = e.Location;
            }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            gr = Graphics.FromHwnd(this.Handle);
        }
    }
}

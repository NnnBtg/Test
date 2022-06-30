using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TestTask.Models;

namespace TestTask
{
    public partial class Form1 : Form
    {
        List<Contur> conturs;
        SRectangle selectedArea;
        bool mouseDown = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var defaultPath = "sss.json";
            conturs = ContursLoader.LoadJson(defaultPath);

            DrawDefaultPolygon();
        }

        private void DrawDefaultPolygon()
        {
            var scene = CreateScene();

            var pen = new Pen(Brushes.Gray, 1);
            foreach (var contur in conturs)
            {
                DrawContur(contur, pen, scene);
            }
        }

        private void DrawSpecialPolygon()
        {
            var scene = CreateScene();

            //Сортируем отрезки 
            var hConturParts = new List<Contur>(); //Highlighted Contur Parts
            var oConturParts = new List<Contur>(); //Other Contur Parts
            foreach (var contur in conturs)
            {
                var hPieces = new List<Piece>();
                var oPieces = new List<Piece>();
                foreach (var piece in contur.Pieces)
                {
                    if (selectedArea.ContainsPiece(piece))
                        hPieces.Add(piece);
                    else
                        oPieces.Add(piece);
                }
                hConturParts.Add(new Contur(hPieces));
                oConturParts.Add(new Contur(oPieces));
            }

            var pen = new Pen(Brushes.Red, 2);
            foreach (var contur in hConturParts)
            {
                DrawContur(contur, pen, scene);
            }

            pen = new Pen(Brushes.Gray, 1);
            foreach (var contur in oConturParts)
            {
                DrawContur(contur, pen, scene);
            }

            DrawRectangle(scene);
        }

        private void DrawContur(Contur contur, Pen pen, Graphics g)
        {
            foreach (var piece in contur.Pieces)
            {
                
                Point[] points =
                {
                    new Point(piece.P1.X, piece.P1.Y),
                    new Point(piece.P2.X, piece.P2.Y )
                };
                g.DrawLines(pen, points);
            }
        }

        private void DrawRectangle(Graphics g)
        {
            var pen = new Pen(Brushes.Green, 1);
            var dRect = new Rectangle(selectedArea.MinX, selectedArea.MinY, selectedArea.Width, selectedArea.Height);
            g.DrawRectangle(pen, dRect);
        }

        private Graphics CreateScene()
        {
            float addw = pictureBox1.Width / 2;
            float addH = pictureBox1.Height / 2;

            Bitmap btm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = btm;
            Graphics g = Graphics.FromImage(btm);
            g.TranslateTransform(addw, addH);
            g.ScaleTransform(1, -1);

            return g;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.GetLocalX(pictureBox1.Width);
            int y = e.GetLocalY(pictureBox1.Height);
            mouseDown = true;
            selectedArea.P1 = new Point2D(x, y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int x = e.GetLocalX(pictureBox1.Width);
            int y = e.GetLocalY(pictureBox1.Height);
            mouseDown = false;
            selectedArea.P2 = new Point2D(x, y);

            DrawSpecialPolygon();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.GetLocalX(pictureBox1.Width);
            int y = e.GetLocalY(pictureBox1.Height);
            label1.Text = $"X : {x}";
            label2.Text = $"Y : {y}";
            if (!mouseDown)
                return;
            selectedArea.P2 = new Point2D(x, y);

            DrawSpecialPolygon();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            DrawSpecialPolygon();
        }
    }
}

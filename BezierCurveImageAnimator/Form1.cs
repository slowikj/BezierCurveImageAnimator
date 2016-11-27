using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using BezierCurveImageAnimator.Bezier;
using BezierCurveImageAnimator.Polylines;

namespace BezierCurveImageAnimator
{
    public partial class Form1 : Form
    {
        private BezierPolyline _polyline;
        private Pen _bezierPen;

        public Form1()
        {
            InitializeComponent();

            _polyline = new BezierPolyline(10, canvas.Width, canvas.Height);
            _bezierPen = new Pen(Color.Black);         
        }
        
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            FastBitmap fastBitmap = new FastBitmap(new Bitmap(canvas.Width, canvas.Height, e.Graphics), false);
            PaintTools paintTools = new PaintTools(canvas, fastBitmap, e.Graphics);

            if (visiblePolylineCheckbox.Checked)
            {
                _polyline.Draw(paintTools);
            }

            _DrawBezierCurve(_bezierPen, _polyline, paintTools);
            
            paintTools.Graphics.DrawImage(paintTools.Bitmap.GetBitmap(), new Point(0, 0));
        }

        private void _DrawBezierCurve(Pen pen, BezierPolyline polyline, PaintTools paintTools)
        {
            _polyline.GetBezierCurve()
                     .Draw(paintTools, pen);

            PointD[] points = _polyline.GetPoints();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _polyline.GetMover().MoveVertex(e);
            this.Repaint(canvas);
        }
        
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            _polyline.GetMover().SetFocus(e);
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _polyline.GetMover().ResetFocus();
        }

        private void generateBezierButton_Click(object sender, EventArgs e)
        {
            try
            {
                _polyline = new BezierPolyline(int.Parse(pointsNumberTextbox.Text),
                                               canvas.Width, canvas.Height);
            }
            catch(ArgumentException exception)
            {
                MessageBox.Show("Incorrect number argument \n" + exception.Message);
            }
            catch(FormatException)
            {
                MessageBox.Show("Incorrect format");
            }

            this.Repaint(canvas);
        }
        
        private void visiblePolylineCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.Repaint(canvas);
        }

        private void Repaint(PictureBox pictureBox)
        {
            pictureBox.Invalidate();
            pictureBox.Update();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Drawing2D;
using System.IO;

using BezierCurveImageAnimator.Bezier;
using BezierCurveImageAnimator.Polylines;
using BezierCurveImageAnimator.Animators;
using BezierCurveImageAnimator.Rotators;

namespace BezierCurveImageAnimator
{
    public partial class Form1 : Form
    {
        private const int _IMAGE_WIDTH = 200, _IMAGE_HEIGHT = 200;
        private const int _DEFAULT_BEZIER_POLY_POINTS = 15;
        private const int _MAX_POLYLINE_POINTS = 40;

        private BezierPolyline _polyline;
        private Pen _bezierPen;
        private Animator _animator;
        private ImageLoader _imageLoader;
        private FastBitmap _image;
        private AnimatorCreator _animatorCreator;

        public Form1()
        {
            InitializeComponent();

            _polyline = new BezierPolyline(_DEFAULT_BEZIER_POLY_POINTS,
                                           canvas.Width, canvas.Height);
            _bezierPen = new Pen(Color.Black);

            _animatorCreator = new AnimatorCreator(_image, _polyline, canvas.Width, canvas.Height);

            _imageLoader = new ImageLoader(_IMAGE_WIDTH, _IMAGE_HEIGHT);
            _SetImage(_imageLoader.GetDefaultImage());
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
            
            if(_animator != null)
            {
                _animator.Draw(paintTools);
            }

            paintTools.Graphics.DrawImage(paintTools.Bitmap.GetBitmap(),
                                          new Point(0, 0));
        }

        private void _DrawBezierCurve(Pen pen, BezierPolyline polyline, PaintTools paintTools)
        {
            _polyline.GetBezierCurve()
                     .Draw(paintTools, pen);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_polyline.GetMover().MoveVertex(e))
            {
                this.Repaint(canvas);
            }
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
                int n = int.Parse(pointsNumberTextbox.Text);
                if(n > _MAX_POLYLINE_POINTS)
                {
                    throw new ArgumentException("the number should be <= 40");
                }

                _polyline = new BezierPolyline(n, canvas.Width, canvas.Height);

                _animatorCreator.SetPolyline(_polyline);
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

        private void timer_Tick(object sender, EventArgs e)
        {
            if(_animator != null)
            {
                _animator.Update();
                this.Repaint(canvas);
            }
        }

        private void startAnimationButton_Click(object sender, EventArgs e)
        {
            _animator = _animatorCreator.Get(grayColorCheckbox.Checked);
            timer.Enabled = true;
        }

        private void stopAnimationButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        private void imageView_Paint(object sender, PaintEventArgs e)
        {
            if(_image != null)
            {
                e.Graphics.DrawImage(new Bitmap(_image.GetBitmap(), imageView.Width, imageView.Height),
                                     0, 0);
            }
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            _SetImage(_imageLoader.GetImage());

            this.Repaint(canvas);
            this.Repaint(imageView);
        }

        private void naiveRotatingButton_CheckedChanged(object sender, EventArgs e)
        {
            if(naiveRotatingButton.Checked)
            {
                _animatorCreator.SetRotator(RotatorType.Naive);
            }
        }

        private void filteringRotatingButton_CheckedChanged(object sender, EventArgs e)
        {
            if(filteringRotatingButton.Checked)
            {
                _animatorCreator.SetRotator(RotatorType.WithFiltering);
            }
        }

        private void rotationAnimationButton_CheckedChanged(object sender, EventArgs e)
        {
            if(rotationAnimationButton.Checked)
            {
                _animatorCreator.SetAnimator(AnimatorType.Spinning);
            }
        }

        private void onCurveAnimation_CheckedChanged(object sender, EventArgs e)
        {
            if(onCurveAnimation.Checked)
            {
                _animatorCreator.SetAnimator(AnimatorType.Bezier);
            }
        }

        private void grayColorCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _animator = _animatorCreator.Get(grayColorCheckbox.Checked);
        }

        private void loadPolylineButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult dialogResult = dialog.ShowDialog();

            List<Point> points = new List<Point>();

            if(dialogResult == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(dialog.FileName))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        string[] p = line.Split(' ');
                        points.Add(new Point(int.Parse(p[0]), int.Parse(p[1])));
                    }
                }

                _polyline = new BezierPolyline(points.ToArray());
                _animatorCreator.SetPolyline(_polyline);

                this.Repaint(canvas);
            }
            
        }
        
        private void savePolylineButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                _polyline.Save(dialog.FileName);
            }
        }

        private void _SetImage(Bitmap image)
        {
            if (image != null)
            {
                _image = new FastBitmap(image);
                _animatorCreator.SetImage(_image);
            }
        }
    }
}

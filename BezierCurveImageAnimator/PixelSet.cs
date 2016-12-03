using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using BezierCurveImageAnimator.Rotators.Polygon;
using BezierCurveImageAnimator.Rotators;

namespace BezierCurveImageAnimator
{
    public class PixelSet
    {
        public Dictionary<Point, Color> _pixels { get; set; }
        public Point[] _corners { get; set; }
        public Point _middle { get; set; }
        public Rotator _rotator;

        public PixelSet(FastBitmap image)
        {
            _pixels = _GetPixels(image);
            _corners = _GetCorners(image);
            _middle = _GetMiddle(image);

            _rotator = new NaiveRotator();
        }

        public PixelSet(Point[] points, Color[] colors, Point middle, Point[] corners)
        {
            _pixels = _GetPixels(points, colors);
            _corners = corners;
            _middle = middle;
        }

        public void Translate(FreeVector v)
        {
            _pixels = _GetTranslatedPixels(v);
            _TranslateCorners(v);
            _TranslateMiddle(v);
        }

        public Color GetPixel(int x, int y)
        {
            Point p = new Point(x, y);
            return _pixels.ContainsKey(p) ? _pixels[p]
                                          : Color.Black;
        }

        public PixelSet GetRotated(float angle)
        {
            return _rotator.GetRotated(this, angle);
        }
        
        public void Draw(PaintTools paintTools)
        {
            foreach(var pixel in _pixels)
            {
                paintTools.Bitmap.SetPixel(pixel.Key.X, pixel.Key.Y,
                                           pixel.Value);
            }
        }

        private Dictionary<Point, Color> _GetTranslatedPixels(FreeVector v)
        {
            Dictionary<Point, Color> res = new Dictionary<Point, Color>();
            
            foreach(var pixel in _pixels)
            {
                res.Add((Point)(pixel.Key + v), pixel.Value);
            }

            return res;
        }

        private void _TranslateCorners(FreeVector v)
        {
            for(int i = 0; i < _corners.Length; ++i)
            {
                _corners[i] = (Point)(_corners[i] + v);
            }
        }

        private void _TranslateMiddle(FreeVector v)
        {
            _middle = (Point)(_middle + v);
        }
        
        private Dictionary<Point, Color> _GetPixels(FastBitmap image)
        {
            Dictionary<Point, Color> res = new Dictionary<Point, Color>();
            for(int i = 0; i < image.Height; ++i)
            {
                for(int j = 0; j < image.Width; ++j)
                {
                    res.Add(new Point(i, j), image.GetPixel(i, j));
                }
            }

            return res;
        }

        private Dictionary<Point, Color> _GetPixels(Point[] points, Color[] colors)
        {
            Dictionary<Point, Color> res = new Dictionary<Point, Color>();
            for(int i = 0; i < points.Length; ++i)
            {
                res.Add(points[i], colors[i]);
            }

            return res;
        }

        private Point[] _GetCorners(FastBitmap image)
        {
            return new Point[] {new Point(0, 0),
                                new Point(0, image.Height),
                                new Point(image.Width, image.Height),
                                new Point(image.Width, 0) };
        }

        private Point _GetMiddle(FastBitmap image)
        {
            return new Point(image.Width / 2, image.Height / 2);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Drawing2D;

using BezierCurveImageAnimator.Rotators.Polygon;

namespace BezierCurveImageAnimator.Rotators
{
    public class FilteringRotator : Rotator
    {
        private Action<Dictionary<int, List<KeyValuePair<int, Color>>>, int, int, Color> _addItemWithKeyX;
        private Action<Dictionary<int, List<KeyValuePair<int, Color>>>, int, int, Color> _addItemWithKeyY;

        public FilteringRotator(FastBitmap image, Point? middle = null)
            : base(image, middle)
        {
            _addItemWithKeyX = (d, x, y, color) => _AddPixel(d, x, new KeyValuePair<int, Color>(y, color));
            _addItemWithKeyY = (d, x, y, color) => _AddPixel(d, y, new KeyValuePair<int, Color>(x, color));
        }

        public override PixelSet GetRotated(float angle)
        {
            Dictionary<int, List<KeyValuePair<int, Color>>> r = _GetDictionary(_image);

            while (angle > 90)
            {
                r = _Rotate(-1, 1, r);
                angle -= 90;
            }
           
            double radiansAngle = (angle * Math.PI / 180);
            float xShear = (float)-Math.Tan(radiansAngle / 2);
            float yShear = (float)Math.Sin(radiansAngle);

            r = _Rotate(xShear, yShear, r);

            List<Point> points = new List<Point>();
            List<Color> colors = new List<Color>();
            foreach (var entry in r)
            {
                foreach (var elem in entry.Value)
                {
                   // points.Add(new Point(entry.Key, elem.Key));
                    points.Add(new Point(elem.Key, entry.Key));
                    colors.Add(elem.Value);
                }
            }

            return new PixelSet(points.ToArray(), colors.ToArray());
        }
        
        private Dictionary<int, List<KeyValuePair<int, Color>>> _Rotate(float xShear, float yShear,
                                   Dictionary<int, List<KeyValuePair<int, Color>>> r) // angle must be <= 90 deg!
        {
            var a = _XShear(r, xShear, _addItemWithKeyX);
            var b = _YShear(a, yShear, _addItemWithKeyY);
            var c = _XShear(b, xShear, _addItemWithKeyY);

            return c;
        }
        
        private Dictionary<int, List<KeyValuePair<int, Color>>> _XShear(Dictionary<int, List<KeyValuePair<int, Color>>> dic,
                         float shear,
                         Action<Dictionary<int, List<KeyValuePair<int, Color>>>, int, int, Color> addElem)
        {
            var res = new Dictionary<int, List<KeyValuePair<int, Color>>>();

            foreach (int y in dic.Keys)
            {
                dic[y].Sort((a, b) => b.Key - a.Key);
                double skew = shear * (y - _middlePoint.Y + 0.5);
                int skewl = (int)Math.Floor(skew);
                double skewf = Math.Abs(skew % 1);
                
                Color oleft = Color.FromArgb(0);
                
                for(int i = 1; i < dic[y].Count; ++i)
                //foreach (KeyValuePair<int, Color> elem in dic[y])
                {
                    KeyValuePair<int, Color> elem = dic[y][i];

                    Color pixel = elem.Value;
                    Color left = Color.FromArgb((byte)(pixel.A * skewf),
                                                (byte)(pixel.R * skewf),
                                                (byte)(pixel.G * skewf),
                                                (byte)(pixel.B * skewf));

                    pixel = Color.FromArgb(pixel.A - left.A + oleft.A,
                                            pixel.R - left.R + oleft.R,
                                            pixel.G - left.G + oleft.G,
                                            pixel.B - left.B + oleft.B);
                    
                    addElem(res, elem.Key + skewl, y, pixel);

                    oleft = left;
                }
                addElem(res, dic[y][0].Key + skewl, y, oleft);
            }

            return res;
        }
        
        private Dictionary<int, List<KeyValuePair<int, Color>>> _YShear(Dictionary<int, List<KeyValuePair<int, Color>>> dic,
                   double shear,
                   Action<Dictionary<int, List<KeyValuePair<int, Color>>>, int, int, Color> addElem)
        {
            var res = new Dictionary<int, List<KeyValuePair<int, Color>>>();

            foreach (int x in dic.Keys)
            {
                dic[x].Sort((a, b) => b.Key - a.Key);
                double skew = shear * (x - _middlePoint.X + 0.5);
                int skewl = (int)Math.Floor(skew);
                double skewf = Math.Abs(skew % 1);

                Color oleft = Color.FromArgb(0);

                for (int i = 1; i < dic[x].Count; ++i)
                //foreach (KeyValuePair<int, Color> elem in dic[x])
                {
                    KeyValuePair<int, Color> elem = dic[x][i];

                    Color pixel = elem.Value;
                    Color left = Color.FromArgb((byte)(pixel.A * skewf),
                                                (byte)(pixel.R * skewf),
                                                (byte)(pixel.G * skewf),
                                                (byte)(pixel.B * skewf));

                    pixel = Color.FromArgb(pixel.A - left.A + oleft.A,
                                            pixel.R - left.R + oleft.R,
                                            pixel.G - left.G + oleft.G,
                                            pixel.B - left.B + oleft.B);
                    
                    addElem(res, x, elem.Key + skewl, pixel);
                    
                    oleft = left;
                }

                addElem(res, x, dic[x][dic[x].Count - 1].Key + skewl, oleft);
            }

            return res;
        }
        
        private void _AddPixel(Dictionary<int, List<KeyValuePair<int, Color>>> p, int key, KeyValuePair<int, Color> elem)
        {
            if (!p.ContainsKey(key))
            {
                p.Add(key, new List<KeyValuePair<int, Color>>());
            }
            p[key].Add(elem);
        }

        private Dictionary<int, List<KeyValuePair<int, Color>>> _GetDictionary(FastBitmap bitmap)
        {
            var res = new Dictionary<int, List<KeyValuePair<int, Color>>>();
            for (int y = 0; y < bitmap.Height; ++y)
            {
                res.Add(y, new List<KeyValuePair<int, Color>>());

                for (int x = 0; x <= bitmap.Width; ++x)
                {
                    res[y].Add(new KeyValuePair<int, Color>(x, bitmap.GetPixel(x, y)));
                }
            }

            return res;
        }

        //private Dictionary<int, List<KeyValuePair<int, Color>>> _XShear(FastBitmap bitmap, float shear)
        //{
        //    var res = new Dictionary<int, List<KeyValuePair<int, Color>>>();
        //    for (int y = 0; y < bitmap.Height; ++y)
        //    {
        //        double skew = shear * (y - _middlePoint.Y + 0.5);
        //        int skewl = (int)Math.Floor(skew);
        //        double skewf = Math.Abs(skew % 1);

        //        Color oleft = Color.FromArgb(0);
        //        for (int x = 0; x < bitmap.Width; ++x)
        //        {
        //            Color pixel = bitmap.GetPixel(bitmap.Width - x, y);
        //            Color left = Color.FromArgb((byte)(pixel.A * skewf),
        //                                         (byte)(pixel.R * skewf),
        //                                         (byte)(pixel.G * skewf),
        //                                         (byte)(pixel.B * skewf));

        //            pixel = Color.FromArgb(pixel.A - left.A + oleft.A,
        //                                   pixel.R - left.R + oleft.R,
        //                                   pixel.G - left.G + oleft.G,
        //                                   pixel.B - left.B + oleft.B);

        //            _AddPixel(res, bitmap.Width - x + skewl, new KeyValuePair<int, Color>(y, pixel));

        //            oleft = left;
        //        }
        //        _AddPixel(res, skewl, new KeyValuePair<int, Color>(y, oleft));
        //    }

        //    return res;
        //}

        //private Dictionary<int, List<KeyValuePair<int, Color>>> _Rotate90(int multiply)
        //{
        //    int angle = 90 * multiply;
        //    var res = new Dictionary<int, List<KeyValuePair<int, Color>>>();

        //    Matrix matrix = new Matrix();
        //    matrix.RotateAt(angle, _middlePoint);

        //    int imageSize = (_image.Width + 1) * (_image.Height);
        //    Point[] points = new Point[imageSize];
        //    Color[] colors = new Color[imageSize];
        //    for (int y = 0; y < _image.Height; ++y)
        //    {
        //        for (int x = 0; x <= _image.Width; ++x)
        //        {
        //            int index = y * _image.Height + x;
        //            points[index] = new Point(x, y);
        //            colors[index] = _image.GetPixel(x, y);
        //        }
        //    }

        //    matrix.TransformPoints(points);

        //    for (int i = 0; i < points.Length; ++i)
        //    {
        //        if (!res.ContainsKey(points[i].Y))
        //        {
        //            res.Add(points[i].Y, new List<KeyValuePair<int, Color>>());
        //        }
        //        res[points[i].Y].Add(new KeyValuePair<int, Color>(points[i].X, colors[i]));
        //    }

        //    return res;
        //}
    }
}

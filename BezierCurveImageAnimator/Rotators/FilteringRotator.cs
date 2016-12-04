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
        public FilteringRotator(FastBitmap image)
            : base(image)
        {
        }

        public override PixelSet GetRotated(float angle)
        {
            Dictionary<int, List<KeyValuePair<int, Color>>> r = _GetDictionary(_image);

            double radiansAngle = (angle * Math.PI / 180);
            double deg90rad = Math.PI / 2;
            double yShear90 = Math.Sin(deg90rad);
            double xShear90 = -Math.Tan(deg90rad / 2);

            while (radiansAngle >= deg90rad)
            {
                r = _Rotate(xShear90, yShear90, r);

                radiansAngle -= deg90rad;
            }

            double xShear = -Math.Tan(radiansAngle / 2);
            double yShear = Math.Sin(radiansAngle);
            r = _Rotate(xShear, yShear, r);
            
            List<Point> points = new List<Point>();
            List<Color> colors = new List<Color>();
            foreach (var entry in r)
            {
                foreach (var elem in entry.Value)
                {
                    //points.Add(new Point(entry.Key, elem.Key));
                    points.Add(new Point(elem.Key, entry.Key));
                    colors.Add(elem.Value);
                }
            }

            return new PixelSet(points.ToArray(), colors.ToArray());
        }

        private Dictionary<int, List<KeyValuePair<int, Color>>> _Rotate(double xShear, double yShear,
                                   Dictionary<int, List<KeyValuePair<int, Color>>> r) // angle must be <= 90 deg!
        {
            var a = _XShear(r, xShear);
            var b = _YShear(a, yShear);
            var c = _XShear2(b, xShear);

            return c;
        }

        //private Dictionary<int, List<KeyValuePair<int, Color>>> _XShear(FastBitmap bitmap, float shear)
        //{
        //    var res = new Dictionary<int, List<KeyValuePair<int, Color>>>();
        //    for (int y = 0; y < bitmap.Height; ++y)
        //    {
        //        double skew = shear * (y + 0.5);
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

        

        private Dictionary<int, List<KeyValuePair<int, Color>>> _XShear(Dictionary<int, List<KeyValuePair<int, Color>>> bitmap,
                                                                       double shear)
        {
            var res = new Dictionary<int, List<KeyValuePair<int, Color>>>();

            foreach (int y in bitmap.Keys)
            {
                bitmap[y].Sort((a, b) => a.Key - b.Key);
                double skew = shear * (y + 0.5);
                int skewl = (int)Math.Floor(skew);
                double skewf = Math.Abs(skew % 1);
                
                Color oleft = Color.FromArgb(0);
                foreach (KeyValuePair<int, Color> elem in bitmap[y])
                {
                    Color pixel = elem.Value;
                    Color left = Color.FromArgb((byte)(pixel.A * skewf),
                                                (byte)(pixel.R * skewf),
                                                (byte)(pixel.G * skewf),
                                                (byte)(pixel.B * skewf));

                    pixel = Color.FromArgb(pixel.A - left.A + oleft.A,
                                            pixel.R - left.R + oleft.R,
                                            pixel.G - left.G + oleft.G,
                                            pixel.B - left.B + oleft.B);

                    _AddPixel(res, elem.Key + skewl, new KeyValuePair<int, Color>(y, pixel));

                    oleft = left;
                }
                _AddPixel(res, bitmap[y][0].Key + skewl, new KeyValuePair<int, Color>(y, oleft));
            }

            return res;
        }

        private Dictionary<int, List<KeyValuePair<int, Color>>> _XShear2(Dictionary<int, List<KeyValuePair<int, Color>>> bitmap,
                                                                       double shear)
        {
            var res = new Dictionary<int, List<KeyValuePair<int, Color>>>();

            foreach (int y in bitmap.Keys)
            {
                bitmap[y].Sort((a, b) => a.Key - b.Key);
                double skew = shear * (y + 0.5);
                int skewl = (int)Math.Floor(skew);
                double skewf = Math.Abs(skew % 1);

                Color oleft = Color.FromArgb(0);
                foreach (KeyValuePair<int, Color> elem in bitmap[y])
                {
                    Color pixel = elem.Value;
                    Color left = Color.FromArgb((byte)(pixel.A * skewf),
                                                (byte)(pixel.R * skewf),
                                                (byte)(pixel.G * skewf),
                                                (byte)(pixel.B * skewf));

                    pixel = Color.FromArgb(pixel.A - left.A + oleft.A,
                                            pixel.R - left.R + oleft.R,
                                            pixel.G - left.G + oleft.G,
                                            pixel.B - left.B + oleft.B);

                    _AddPixel(res, y, new KeyValuePair<int, Color>(elem.Key + skewl, pixel));

                    oleft = left;
                }
                _AddPixel(res, y, new KeyValuePair<int, Color>(bitmap[y][0].Key + skewl, oleft));
            }

            return res;
        }

        private Dictionary<int, List<KeyValuePair<int, Color>>> _YShear(Dictionary<int, List<KeyValuePair<int, Color>>> bitmap,
                                                                      double shear)
        {
            var res = new Dictionary<int, List<KeyValuePair<int, Color>>>();

            foreach (int x in bitmap.Keys)
            {
                bitmap[x].Sort((a, b) => a.Key - b.Key);
                double skew = shear * (x + 0.5);
                int skewl = (int)Math.Floor(skew);
                double skewf = Math.Abs(skew % 1);

                Color oleft = Color.FromArgb(0);
                foreach (KeyValuePair<int, Color> elem in bitmap[x])
                {
                    Color pixel = elem.Value;
                    Color left = Color.FromArgb((byte)(pixel.A * skewf),
                                                (byte)(pixel.R * skewf),
                                                (byte)(pixel.G * skewf),
                                                (byte)(pixel.B * skewf));

                    pixel = Color.FromArgb(pixel.A - left.A + oleft.A,
                                            pixel.R - left.R + oleft.R,
                                            pixel.G - left.G + oleft.G,
                                            pixel.B - left.B + oleft.B);

                    _AddPixel(res, elem.Key + skewl, new KeyValuePair<int, Color>(x, pixel));

                    oleft = left;
                }

                _AddPixel(res, bitmap[x][0].Key + skewl, new KeyValuePair<int, Color>(x, oleft));
            }

            return res;
        }

        private Dictionary<int, List<KeyValuePair<int, Color>>> _GetDictionary(FastBitmap bitmap)
        {
            var res = new Dictionary<int, List<KeyValuePair<int, Color>>>();
            for (int y = 0; y < bitmap.Height; ++y)
            {
                res.Add(y, new List<KeyValuePair<int, Color>>());

                for (int x = bitmap.Width; x >= 0; --x)
                {
                    res[y].Add(new KeyValuePair<int, Color>(x, bitmap.GetPixel(x, y)));
                }
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
    }
}

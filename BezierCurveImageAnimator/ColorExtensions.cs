using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BezierCurveImageAnimator
{
    public static class ColorExtensions
    {
        public static Color Multiply(this Color c, double d)
        {
            return Color.FromArgb((byte)(c.A * d),
                                  (byte)(c.R * d),
                                  (byte)(c.G * d),
                                  (byte)(c.B * d));
        }

        public static Color Subtract(this Color a, Color b)
        {
            return Color.FromArgb(a.A - b.A,
                                  a.R - b.R,
                                  a.G - b.G,
                                  a.B - b.B);
        }
    }
}

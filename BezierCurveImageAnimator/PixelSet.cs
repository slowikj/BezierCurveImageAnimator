using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace BezierCurveImageAnimator
{
    public class PixelSet
    {
        public Color[] Colors { get; set; }
        public Point[] Locations { get; set; }

        public PixelSet(FastBitmap image)
        {
            this.Colors = _GetInitialColors(image);
            this.Locations = _GetInitialCoordinates(image.Width, image.Height);
        }

        public PixelSet(Point[] locations, Color[] colors)
        {
            this.Locations = locations;
            this.Colors = colors;
        }
        
        private Color[] _GetInitialColors(FastBitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            int length = width * height;
            Color[] res = new Color[length];

            int cnt = 0;
            for (int i = 0; i < height; ++i)
            {
                for (int j = width - 1; j >= 0; --j)
                {
                    res[cnt++] = image.GetPixel(i, j);
                }
            }

            return res;
        }

        private Point[] _GetInitialCoordinates(int width, int height)
        {
            int length = width * height;
            Point[] res = new Point[length];

            int cnt = 0;
            for (int i = 0; i < height; ++i)
            {
                for (int j = width - 1; j >= 0; --j)
                {
                    res[cnt++] = new Point(i, j);
                }
            }

            return res;
        }
    }
}

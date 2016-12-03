using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace BezierCurveImageAnimator.Rotators
{
    public enum RotatorType { Naive, WithFiltering };

    public abstract class Rotator
    {
        protected FastBitmap _image;
        protected Point _middlePoint;

        public Rotator(FastBitmap image)
        {
            _image = image;

            _middlePoint = new Point(image.Width / 2, image.Height / 2);
        }
        
        public abstract PixelSet GetRotated(float angle);
    }
}

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
        public Rotator()
        {
        }
        
        public abstract PixelSet GetRotated(PixelSet pixelSet, float angle);
    }
}

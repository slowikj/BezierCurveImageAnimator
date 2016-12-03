using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace BezierCurveImageAnimator.Transformators.Rotators
{
    public abstract class Rotator : ITransformator
    {
        protected float _angle;

        public Rotator(float angle)
        {
            _angle = angle;
        }
        
        public abstract void Process(PixelSet pixelSet);
    }
}

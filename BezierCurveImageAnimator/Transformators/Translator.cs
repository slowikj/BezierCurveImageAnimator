using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace BezierCurveImageAnimator.Transformators
{
    public class Translator : ITransformator
    {
        private FreeVector _vector;

        public Translator(FreeVector vector)
        {
            _vector = vector;
        }

        public void Process(PixelSet pixelSet)
        {
            pixelSet.MiddlePoint = (Point)(pixelSet.MiddlePoint + _vector);
            
            foreach(Point point in pixelSet.GetPoints())
            {
                pixelSet.ChangeLocation(point, (Point)(point + _vector));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierCurveImageAnimator.Transformators
{
    public interface ITransformator
    {
        void Process(PixelSet pixelSet);
    }
}

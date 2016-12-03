using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierCurveImageAnimator.Transformators.Rotators
{
    public class FilteringRotator : Rotator
    {
        public FilteringRotator(FastBitmap image)
            : base(image)
        {
        }

        public override PixelSet GetRotated(float angle)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using BezierCurveImageAnimator.Polylines;

namespace BezierCurveImageAnimator.Bezier
{
    public class BezierPolyline : Polyline
    {
        private BezierCurve _bezierCurve;
        private const int CURVE_POINTS_NUMBER = 200;

        public BezierPolyline(int n, int canvasWidth, int canvasHeight)
            : base(n, canvasWidth, canvasHeight)
        {
        }

        public BezierCurve GetBezierCurve()
        {
            if(_bezierCurve == null)
            {
                _bezierCurve = new BezierCurve(this,
                                               BezierPolyline.CURVE_POINTS_NUMBER);
            }

            return _bezierCurve;
        }
    }
}

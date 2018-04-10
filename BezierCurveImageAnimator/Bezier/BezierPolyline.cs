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
        private const int CURVE_POINTS_NUMBER = 200;
        private BezierCurve _bezierCurve;
        private int _curvePointsNumber;

        public int CurvePointsNumber
        {
            get
            {
                return _curvePointsNumber;
            }
        }

        public BezierPolyline(int n, int canvasWidth, int canvasHeight,
                              int curvePointsNumber = CURVE_POINTS_NUMBER)
            : base(n, canvasWidth, canvasHeight)
        {
            _curvePointsNumber = curvePointsNumber;
        }

        public BezierPolyline(Point[] points, int curverPointsNumber = CURVE_POINTS_NUMBER)
            : base(points)
        {
            _curvePointsNumber = curverPointsNumber;
        }

        public BezierCurve GetBezierCurve()
        {
            if(_bezierCurve == null)
            {
                _bezierCurve = new BezierCurve(this,
                                               _curvePointsNumber);
            }

            return _bezierCurve;
        }
    }
}

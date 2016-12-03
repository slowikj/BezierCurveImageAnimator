using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Drawing2D;

using BezierCurveImageAnimator.Bezier;
using BezierCurveImageAnimator.Polylines;

namespace BezierCurveImageAnimator.Animators
{
    public class BezierMoveAnimator : Animator
    {
        private int _bezierPointNumber;
        private BezierPolyline _polyline;
        private Point _imageMiddle;

        public BezierMoveAnimator(FastBitmap image, BezierPolyline polyline)
            : base(image)
        {
            _bezierPointNumber = 0;
            _polyline = polyline;
            _imageMiddle = new Point(image.Width / 2, image.Height / 2);
        }

        public override void Draw(PaintTools paintTools)
        {
            BezierCurve bezierCurve = _polyline.GetBezierCurve();
            FreeVector tangentVector = bezierCurve.GetTangentVector(_bezierPointNumber);
            Point curvePoint = bezierCurve.GetPoint(_bezierPointNumber);

            float angle = (new FreeVector(new Point(1,0))).GetAngleWith(tangentVector);
            PixelSet rotatedObject = _pixelSet.GetRotated(angle);
            FreeVector translateVector = new FreeVector(_imageMiddle, curvePoint);
            rotatedObject.Translate(translateVector);

            rotatedObject.Draw(paintTools);
        }

        public override void Update()
        {
            _bezierPointNumber++;
            if(_bezierPointNumber >= _polyline.CurvePointsNumber)
            {
                _bezierPointNumber -= _polyline.CurvePointsNumber;
            }
        }
    }
}

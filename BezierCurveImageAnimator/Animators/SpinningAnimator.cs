using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace BezierCurveImageAnimator.Animators
{
    public class SpinningAnimator : Animator
    {
        private const int _DEFAULT_ANGLE = 10;
        private int _currentAngle;
        private FreeVector _moveVector;
        
        public SpinningAnimator(FastBitmap image, int canvasWidth, int canvasHeight)
            : base(image)
        {
            _currentAngle = 0;
            _moveVector = _GetMoveVectorToCenter(image, canvasHeight, canvasHeight);
        }

        public override void Draw(PaintTools paintTools)
        {
            PixelSet pixels = _rotator.GetRotated(_currentAngle);

            for(int i = 0; i < pixels.Locations.Length; ++i)
            {
                Point translatedPoint = (Point)(pixels.Locations[i] + _moveVector);
                paintTools.Bitmap.SetPixel(translatedPoint.X,
                                           translatedPoint.Y,
                                           pixels.Colors[i]);
            }
        }

        public override void Update()
        {
            _currentAngle += _DEFAULT_ANGLE;
            if(_currentAngle >= 360)
            {
                _currentAngle -= 360;
            }
        }

        private FreeVector _GetMoveVectorToCenter(FastBitmap image, int canvasWidth, int canvasHeight)
        {
            return new FreeVector(new PointD(image.Width / 2, image.Height / 2),
                                  new PointD(canvasWidth / 2, canvasHeight / 2));
        }
    }
}

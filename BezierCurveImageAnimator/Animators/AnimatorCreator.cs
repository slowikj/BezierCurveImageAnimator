using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BezierCurveImageAnimator.Rotators;
using BezierCurveImageAnimator.Animators;
using BezierCurveImageAnimator.Bezier;

namespace BezierCurveImageAnimator.Animators
{
    public class AnimatorCreator
    {
        private AnimatorType _animatorType;
        private RotatorType _rotatorType;
        private FastBitmap _image;
        private BezierPolyline _bezierPolyline;
        private int _canvasWidth, _canvasHeight;

        public AnimatorCreator(FastBitmap image, BezierPolyline bezierPolyline, int canvasWidth, int canvasHeight) 
        {
            _animatorType = AnimatorType.Spinning;
            _rotatorType = RotatorType.Naive;
            _image = image;
            _bezierPolyline = bezierPolyline;

            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;
        }

        public void SetAnimator(AnimatorType animatorType)
        {
            _animatorType = animatorType;
        }

        public void SetRotator(RotatorType rotatorType)
        {
            _rotatorType = rotatorType;
        }

        public void SetImage(FastBitmap image)
        {
            _image = image;
        }

        public void SetPolyline(BezierPolyline polyline)
        {
            _bezierPolyline = polyline;
        }

        public Animator Get()
        {
            Animator animator = _GetAnimator();
            animator.SetRotator(_rotatorType);

            return animator;
        }

        private Animator _GetAnimator()
        {
            switch (_animatorType)
            {
                case AnimatorType.Bezier:
                    return new BezierMoveAnimator(_image, _bezierPolyline);
                case AnimatorType.Spinning:
                    return new SpinningAnimator(_image, _canvasWidth, _canvasHeight);
                default:
                    throw new Exception("bad animator type");
            }
        }
    }
}

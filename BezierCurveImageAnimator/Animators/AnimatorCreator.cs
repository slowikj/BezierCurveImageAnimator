using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Drawing2D;

using BezierCurveImageAnimator.Rotators;
using BezierCurveImageAnimator.Animators;
using BezierCurveImageAnimator.Bezier;

namespace BezierCurveImageAnimator.Animators
{
    public class AnimatorCreator
    {
        private AnimatorType _animatorType;
        private RotatorType _rotatorType;
        private FastBitmap _image, _grayImage;
        private BezierPolyline _bezierPolyline;
        private int _canvasWidth, _canvasHeight;

        public AnimatorCreator(FastBitmap image, BezierPolyline bezierPolyline, int canvasWidth, int canvasHeight) 
        {
            _animatorType = AnimatorType.Spinning;
            _rotatorType = RotatorType.Naive;
            _image = image;
            //_grayImage = _GetGrayImage(_image);
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
            _grayImage = _GetGrayImage(image);
        }

        public void SetPolyline(BezierPolyline polyline)
        {
            _bezierPolyline = polyline;
        }

        public Animator Get(bool gray)
        {
            Animator animator = _GetAnimator(gray ? _grayImage : _image);
            animator.SetRotator(_rotatorType);

            return animator;
        }

        private Animator _GetAnimator(FastBitmap image)
        {
            switch (_animatorType)
            {
                case AnimatorType.Bezier:
                    return new BezierMoveAnimator(image, _bezierPolyline);
                case AnimatorType.Spinning:
                    return new SpinningAnimator(image, _canvasWidth, _canvasHeight);
                default:
                    throw new Exception("bad animator type");
            }
        }

        private FastBitmap _GetGrayImage(FastBitmap image)
        {
            FastBitmap res = new FastBitmap(image.GetBitmap());

            for (int i = 0; i < res.Width; ++i)
            {
                for (int j = 0; j < res.Height; ++j)
                {
                    Color pixel = image.GetPixel(i, j);
                    byte I = (byte)((pixel.R * 0.299) + (pixel.G * 0.587) + (pixel.B * 0.114));


                    res.SetPixel(i, j, Color.FromArgb(pixel.A, I, I, I));
                }
            }

            return res;
        }
    }
}

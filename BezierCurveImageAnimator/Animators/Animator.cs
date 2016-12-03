using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using BezierCurveImageAnimator.Rotators;

namespace BezierCurveImageAnimator.Animators
{
    public enum AnimatorType { Bezier, Spinning };

    public abstract class Animator
    {
        private Dictionary<string, Rotator> _rotators;
        protected PixelSet _pixelSet;
        
        public Animator(FastBitmap image)
        {
            _rotators = _GetRotators(image);

            _pixelSet = new PixelSet(image);

            this.SetRotator(RotatorType.Naive);
        }

        public abstract void Update();
        public abstract void Draw(PaintTools paintTools);        

        public void SetRotator(RotatorType rotatorType)
        {
            switch (rotatorType)
            {
                case RotatorType.Naive: _pixelSet._rotator = _rotators["Naive"]; break;
                case RotatorType.WithFiltering: _pixelSet._rotator = _rotators["WithFiltering"]; break;
                default: throw new ArgumentException("Bad rotator");
            }
        }

        private Dictionary<string, Rotator> _GetRotators(FastBitmap image)
        {
            Dictionary<string, Rotator> res = new Dictionary<string, Rotator>();
            
            res["Naive"] = new NaiveRotator();
            res["WithFiltering"] = new FilteringRotator();

            return res;
        }
    }
}

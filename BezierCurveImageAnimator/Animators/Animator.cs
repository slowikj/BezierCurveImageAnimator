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
    public abstract class Animator
    {
        private Dictionary<string, Rotator> _rotators;
        
        protected Rotator _rotator;

        public Animator(FastBitmap image)
        {
            _rotators = _GetRotators(image);

            this.SetRotator("Naive");
        }

        public abstract void Update();
        public abstract void Draw(PaintTools paintTools);        

        public void SetRotator(string name)
        {
            switch (name)
            {
                case "Naive": _rotator = _rotators["Naive"]; break;
                case "WithFiltering": _rotator = _rotators["WithFiltering"]; break;
                default: throw new ArgumentException("Bad rotator");
            }
        }

        private Dictionary<string, Rotator> _GetRotators(FastBitmap image)
        {
            Dictionary<string, Rotator> res = new Dictionary<string, Rotator>();
            
            res["Naive"] = new NaiveRotator(image);
            res["WithFiltering"] = new FilteringRotator(image);

            return res;
        }
    }
}

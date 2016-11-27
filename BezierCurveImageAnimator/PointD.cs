using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BezierCurveImageAnimator
{
    public struct PointD
    {
        private double _x;
        private double _y;

        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public PointD(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public static implicit operator PointD(PointF p)
        {
            return new PointD(p.X, p.Y);
        }

        public static implicit operator PointD(Point p)
        {
            return new PointD(p.X, p.Y);
        }

        public static explicit operator Point(PointD p)
        {
            return new Point((int)p.X, (int)p.Y);
        }

        public static explicit operator PointF(PointD p)
        {
            return new PointF((float)p.X, (float)p.Y);
        }

        public static bool operator==(PointD a, PointD b)
        {
            return PointD.EqualsEps(a.X, b.X)
                && PointD.EqualsEps(a.Y, b.Y);
        }

        public static bool operator !=(PointD a, PointD b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", _x, _y);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PointD))
            {
                return false;
            }

            PointD other = (PointD)obj;

            return this == other;
        }

        public override int GetHashCode()
        {
            return ((PointF)this).GetHashCode();
        }

        public static bool EqualsEps(double a, double b, double eps = 0.0001)
        {
            return Math.Abs(a - b) <= eps;
        }

        public static PointD operator* (PointD p, double r)
        {
            return new PointD(p._x * r,
                              p._y * r);
        }

        public static PointD operator+ (PointD a, PointD b)
        {
            return new PointD(a._x + b._x,
                              a._y + b._y);
        }

        public static PointD operator- (PointD a, PointD b)
        {
            return new PointD(a._x - b._x,
                              a._y - b._y);
        }
    }
}

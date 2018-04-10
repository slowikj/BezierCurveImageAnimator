using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierCurveImageAnimator.Rotators.Polygon
{
    public struct Segment
    {
        private PointD _beg, _end;
        private Pen _pen;

        public Color Color
        {
            get
            {
                return _pen.Color;
            }
            set
            {
                _pen.Color = value;
            }
        }

        public PointD Begin
        {
            get
            {
                return _beg;
            }
        }

        public PointD End
        {
            get
            {
                return _end;
            }
        }

        public PointD Middle
        {
            get
            {
                return new PointD((_beg.X + _end.X) / 2,
                                 (_beg.Y + _end.Y) / 2);
            }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt((_beg.X - End.X) * (_beg.X - End.X)
                                      + (_beg.Y - End.Y) * (_beg.Y - End.Y));
            }
        }

        public bool IsVertical
        {
            get
            {
                return _beg.X == _end.X;
            }
        }

        public bool IsHorizontal
        {
            get
            {
                return _beg.Y == _end.Y;
            }
        }


        public Segment(PointD beg, PointD end, Color? color = null)
        {
            _beg = new Point();
            _end = new Point();
            _pen = new Pen(color ?? Color.Black);

            _AssignPoints(beg, end);
        }

        private void _AssignPoints(PointD a, PointD b)
        {
            _beg = _GetLowerPoint(a, b);
            _end = _GetUpperPoint(a, b);
        }

        private static PointD _GetLowerPoint(PointD a, PointD b)
        {
            if (a.Y != b.Y)
            {
                return a.Y > b.Y ? a
                                 : b;
            }
            else
            {
                return a.X > b.X ? b
                                 : a;
            }
        }

        private static PointD _GetUpperPoint(PointD a, PointD b)
        {
            if (a.Y != b.Y)
            {
                return a.Y > b.Y ? b
                                 : a;
            }
            else
            {
                return a.X > b.X ? a
                                 : b;
            }
        }

        public void Draw(PaintTools paintTools)
        {
            paintTools.Graphics.DrawLine(_pen, (PointF)_beg, (PointF)_end);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace BezierCurveImageAnimator
{
    public class Vertex : IComparable<Vertex>
    {
        private readonly Color _DEFAULT_COLOR = Color.Red; 
        private const double _DEFAULT_RADIUS = 5.0;
        private Point _middle;
        private SolidBrush _brush;
        private double _radius;

        public Point Location
        {
            get
            {
                return _middle;
            }
            set
            {
                _middle = value;
            }
        }

        public Color Color
        {
            get
            {
                return _brush.Color;
            }
        }

        public Vertex(Point point, Color? color = null, double radius = _DEFAULT_RADIUS)
        {
            _middle = point;
            _brush = new SolidBrush(color ?? _DEFAULT_COLOR);
            _radius = radius;
        }

        public void Draw(PaintTools paintTools)
        {
            paintTools.Graphics.FillEllipse(_brush, (float)(_middle.X - _radius), (float)(_middle.Y - _radius),
                                     (float)(_radius + _radius), (float)(_radius + _radius));
        }

        public bool IsClickedBy(Point p)
        {
            return _DistanceSquared(_middle, p) <= _radius * _radius;
        }

        public Point GetAngleRotated(Point p, double angle)
        {
            Point res = new Point();
            res.X = (int)((_middle.X - p.X) * Math.Cos(angle) - (_middle.Y - p.Y) * Math.Sin(angle) + p.X);
            res.Y = (int)((_middle.X - p.X) * Math.Sin(angle) - (_middle.Y - p.Y) * Math.Cos(angle) + p.Y);

            return res;
        }

        private double _DistanceSquared(Point a, Point b)
        {
            Func<int, int> Sqr = (x => x * x);

            return Sqr(a.X - b.X) + Sqr(a.Y - b.Y);
        }

        public int CompareTo(Vertex other)
        {
            return this.Location.X != other.Location.X ? this.Location.X - other.Location.X
                                                       : -(this.Location.Y - other.Location.Y);
        }
    }
}

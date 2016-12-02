using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BezierCurveImageAnimator.Polylines;

namespace BezierCurveImageAnimator.Bezier
{
    public class BezierCurve
    {
        private Polyline _polyline;
        private double[][] _bezierFactors, _derivativeFactors; // b_{i,n}(t) --> b[t][i]
        private int _curvePointsNumber;

        public int CurvePointsNumber
        {
            get
            {
                return _curvePointsNumber;
            }
        }

        public BezierCurve(Polyline polyline, int curvePointsNumber)
        {
            if(polyline.NumberOfVertices < 3)
            {
                throw new ArgumentException("number of vertices in the polyline should be >= 3");
            }

            _polyline = polyline;
            _curvePointsNumber = curvePointsNumber;
            
            _bezierFactors = _GetFactors(curvePointsNumber, _polyline.NumberOfVertices,
                                         (a, b) => _GetBt(a, b));

            _derivativeFactors = _GetFactors(curvePointsNumber, _polyline.NumberOfVertices,
                                             (a, b) => _GetDerivativeBt(a, b));
        }

        public Point GetPoint(int index)
        {
            PointD[] points = _polyline.GetPoints();

            return (Point)_bezierFactors[index].Zip(points, (b, point) => point * b)
                                               .Aggregate((a, b) => a + b);
        }
        
        public FreeVector GetTangentVector(int index)
        {
            IEnumerable<PointD> pointsDifferences = _GetNeighbourPointsDifferences(_polyline.GetPoints());

            return new FreeVector(_derivativeFactors[index].Zip(pointsDifferences, (factor, p) => p * factor)
                                                           .Aggregate((a, b) => a + b));
        }

        private IEnumerable<PointD> _GetNeighbourPointsDifferences(PointD[] points)
        {
            return Enumerable.Range(0, points.Length - 1)
                             .Select(i => points[i + 1] - points[i]);                             
        }

        public void Draw(PaintTools paintTools, Pen pen)
        {
            for(int i = 0; i < _curvePointsNumber - 1; ++i)
            {
                paintTools.Graphics.DrawLine(pen,
                                             (Point)this.GetPoint(i),
                                             (Point)this.GetPoint(i + 1));
            }   
        }

        private double[][] _GetFactors(int curvePointsNumber, int polylinePoints,
                                       Func<double, int, double[]> getFactor)
        {
            double[][] res = new double[curvePointsNumber][];

            double d = 1.0 / (curvePointsNumber - 1);
            for(int i = 0; i < curvePointsNumber; ++i)
            {
                res[i] = getFactor(d * i, polylinePoints);
            }

            return res;
        }

        private double[] _GetDerivativeBt(double t, int polylinePointsNumber)
        {
            int n = polylinePointsNumber;

            double[] res = _GetBt(t, n - 1);
            for(int i = 0;  i < res.Length; ++i)
            {
                res[i] *= (n - 1);
            }

            return res;
        }
        
        private double[] _GetBt(double t, int polylinePoints)
        {
            int n = polylinePoints;
            
            double[] res = new double[n];

            if (t == 0.0)
            {
                res[0] = 1;
                for (int i = 1; i < n; ++i)
                {
                    res[i] = 0;
                }

                return res;
            }

            if (t == 1.0)
            {
                res[n - 1] = 1;
                for(int i = 0; i < n - 1; ++i)
                {
                    res[i] = 0;
                }

                return res;
            }

            res[0] = Math.Pow(1 - t, n - 1);
            
            for(int i = 1; i < n; ++i)
            {
                res[i] = res[i - 1] * (n - i) * t / (i * (1 - t));
            }

            return res;
        }
    }
}

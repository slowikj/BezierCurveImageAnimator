using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Drawing2D;

using BezierCurveImageAnimator.Transformators.Rotators.Polygon;
using BezierCurveImageAnimator;

namespace BezierCurveImageAnimator.Transformators.Rotators
{
    public class NaiveRotator : Rotator
    {
        public NaiveRotator(float angle)
            : base(angle)
        {
        }
        
        public override void Process(PixelSet pixelSet)
        {
            _RotateCorners(pixelSet);
            Point[] rotatedPoints = (new PolygonPointsGenerator(pixelSet.Corners)).GetPoints();

            Point[] initialPoints = _GetInitialPoints(_angle, rotatedPoints, pixelSet.MiddlePoint);

            Color[] colors = new Color[initialPoints.Length];
            for (int i = 0; i < initialPoints.Length; ++i)
            {
                colors[i] = pixelSet.GetColor(initialPoints[i].X, initialPoints[i].Y);
            }

            pixelSet.SetPixels(rotatedPoints, colors);
        }

        private void _RotateCorners(PixelSet pixelSet)
        {
            Matrix rotationMatrix = new Matrix();
            rotationMatrix.RotateAt(_angle, pixelSet.MiddlePoint);
            rotationMatrix.TransformPoints(pixelSet.Corners);
        }

        private Point[] _GetInitialPoints(float angle, Point[] rotatedPoints, Point middlePoint)
        {
            Point[] res = (Point[])rotatedPoints.Clone();
            Matrix reversedRotationMatrix = new Matrix();

            reversedRotationMatrix.RotateAt(angle, middlePoint);
            reversedRotationMatrix.Invert();
            
            reversedRotationMatrix.TransformPoints(res);

            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Drawing2D;

using BezierCurveImageAnimator.Rotators.Polygon;

namespace BezierCurveImageAnimator.Rotators
{
    public class NaiveRotator : Rotator
    {
        public NaiveRotator(FastBitmap image)
            : base(image)
        {
        }

        public override PixelSet GetRotated(float angle)
        {
            //PixelSet res = new PixelSet(_image);

            //_rotationMatrix.Reset();
            //_rotationMatrix.RotateAt(angle, _middleIndex);

            //_rotationMatrix.TransformPoints(res.Locations);

            //return res;
            
            Point[] rotatedPoints = (new PolygonPointsGenerator(_GetRotatedCorners(angle))).GetPoints();
            Point[] initialPoints = _GetInitialPoints(angle, rotatedPoints);

            Color[] colors = new Color[initialPoints.Length];
            for (int i = 0; i < initialPoints.Length; ++i)
            {
                colors[i] = _image.GetPixel(initialPoints[i].X, initialPoints[i].Y);
            }

            return new PixelSet(rotatedPoints, colors);
        }

        private Point[] _GetInitialPoints(float angle, Point[] rotatedPoints)
        {
            Point[] res = (Point[])rotatedPoints.Clone();
            Matrix reversedRotationMatrix = new Matrix();

            reversedRotationMatrix.RotateAt(angle, _middlePoint);
            reversedRotationMatrix.Invert();
            
            reversedRotationMatrix.TransformPoints(res);

            return res;
        }

        private Point[] _GetRotatedCorners(float angle)
        {
            Point[] res = _GetRectangleCorners();
            Matrix rotationMatrix = new Matrix();
            rotationMatrix.RotateAt(angle, _middlePoint);
            rotationMatrix.TransformPoints(res);

            return res;
        }
    }
}

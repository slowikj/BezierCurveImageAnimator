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
        public NaiveRotator()
        {
        }

        public override PixelSet GetRotated(PixelSet pixelSet, float angle)
        {
            Point[] corners = _GetRotatedCorners(pixelSet._corners, pixelSet._middle, angle);
            Point[] rotatedPoints = (new PolygonPointsGenerator(corners)).GetPoints();
            Point[] initialPoints = _GetInitialPoints(angle, rotatedPoints, pixelSet._middle);

            Color[] colors = new Color[initialPoints.Length];
            for (int i = 0; i < rotatedPoints.Length; ++i)
            {
                colors[i] = pixelSet.GetPixel(initialPoints[i].X, initialPoints[i].Y);
            }

            return new PixelSet(rotatedPoints, colors, pixelSet._middle, corners);
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

        private Point[] _GetRotatedCorners(Point[] corners, Point middlePoint, float angle)
        {
            Point[] res = (Point[])corners.Clone();
            Matrix rotationMatrix = new Matrix();
            rotationMatrix.RotateAt(angle, middlePoint);
            rotationMatrix.TransformPoints(res);

            return res;
        }
    }
}

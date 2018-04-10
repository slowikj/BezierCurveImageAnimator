using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BezierCurveImageAnimator
{
    public class PaintTools
    {
        private PictureBox _canvas;
        private FastBitmap _bitmap;
        private Graphics _graphics;

        public PictureBox Canvas
        {
            get
            {
                return _canvas;
            }
        }

        public FastBitmap Bitmap
        {
            get
            {
                return _bitmap;
            }
        }

        public Graphics Graphics
        {
            get
            {
                return _graphics;
            }
        }

        public PaintTools(PictureBox canvas, FastBitmap bitmap, Graphics g)
        {
            _canvas = canvas;
            _bitmap = bitmap;
            _graphics = g;
        }
    }
}

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
    public class ImageLoader
    {
        private const string _DEFAULT_IMAGE_PATH = "..\\..\\Resources\\wild_cat.jpg";
        private int _width, _height;

        public ImageLoader(int width, int height)
        {
            _width = width;
            _height = height;
        }
       
        public Bitmap GetImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                return _GetScaledImage(openFileDialog.FileName);
            }

            return null;
        }
        
        public Bitmap GetDefaultImage()
        {
            return _GetScaledImage(_DEFAULT_IMAGE_PATH);
        }

        private Bitmap _GetScaledImage(string fileName)
        {
            Bitmap bitmap = new Bitmap(fileName);

            return new Bitmap(bitmap, _width, _height);
        }
    }
}

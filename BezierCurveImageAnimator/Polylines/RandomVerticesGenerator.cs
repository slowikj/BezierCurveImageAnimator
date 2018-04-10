using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace BezierCurveImageAnimator.Polylines
{
    public class RandomVerticesGenerator
    {
        private Random _random;
        private int _canvasWidth;
        private int _canvasHeight;

        public RandomVerticesGenerator(int canvasWidth, int canvasHeight)
        {
            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;

            _random = new Random();
        }

        public List<Vertex> Next(int n)
        {
            List<Vertex> res = new List<Vertex>();

            for(int i = 0; i < n; ++i)
            {
                res.Add(_GetRandomVertex());
            }

            res.Sort();

            return res;
        }

        private Vertex _GetRandomVertex()
        {
            return new Vertex(new Point(_random.Next(50, _canvasWidth * 4 / 5),
                                        _random.Next(6, _canvasHeight * 4 / 5)));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace BezierCurveImageAnimator.Polylines
{
    public class Polyline
    {
        private const int NO_VERTEX = -1;

        private List<Vertex> _vertices;
        private Pen _pen;
        private PolylineMover _mover;

        public Vertex this[int i]
        {
            get
            {
                return _vertices[i];
            }
            set
            {
                _vertices[i] = value;
            }
        }

        public int NumberOfVertices
        {
            get
            {
                return _vertices.Count;
            }
        }

        public Polyline(int n, int canvasWidth, int canvasHeight)
        {
            if(n < 3)
            {
                throw new ArgumentException("number of vertices in the polyline should be >= 3");
            }

            RandomVerticesGenerator random = new RandomVerticesGenerator(canvasWidth,
                                                                         canvasHeight);

            _vertices = random.Next(n);
            _pen = new Pen(Color.Cyan);

            _mover = new PolylineMover(this);
        }

        public Polyline(Point[] points)
        {
            _vertices = new List<Vertex>();
            foreach(Point p in points)
            {
                _vertices.Add(new Vertex(p));
            }

            _mover = new PolylineMover(this);
            _pen = new Pen(Color.Cyan);
        }
        
        public void Save(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach(Vertex vertex in _vertices)
                {
                    writer.WriteLine(String.Format("{0} {1}", vertex.Location.X, vertex.Location.Y));
                }
            }
        }
        
        public PointD[] GetPoints()
        {
            PointD[] res = new PointD[_vertices.Count];
            for(int i = 0; i < _vertices.Count; ++i)
            {
                res[i] = _vertices[i].Location;
            }

            return res;
        }

        public void Draw(PaintTools paintTools)
        {
            _DrawSegments(paintTools);
            _DrawVertices(paintTools);
        }

        public int GetIndexClickedBy(Point p)
        {
            for(int i = 0; i < this.NumberOfVertices; ++i)
            {
                if(_vertices[i].IsClickedBy(p))
                {
                    return i;
                }
            }

            return NO_VERTEX;
        }
         
        public PolylineMover GetMover()
        {
            return _mover;
        }

        private void _DrawSegments(PaintTools paintTools)
        {
            for(int i = 0; i < this.NumberOfVertices - 1; ++i)
            {
                paintTools.Graphics.DrawLine(_pen,
                                             _vertices[i].Location,
                                             _vertices[i + 1].Location);
            }
        }

        private void _DrawVertices(PaintTools paintTools)
        {
            foreach(Vertex vertex in _vertices)
            {
                vertex.Draw(paintTools);
            }
        }
    }
}

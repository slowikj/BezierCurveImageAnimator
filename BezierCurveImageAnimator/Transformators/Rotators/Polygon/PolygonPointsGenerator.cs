using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BezierCurveImageAnimator;

namespace BezierCurveImageAnimator.Transformators.Rotators.Polygon
{
    public partial class PolygonPointsGenerator
    {
        private Point[] _vertices;

        public PolygonPointsGenerator(Point[] vertices)
        {
            _vertices = vertices;
        }

        public Point[] GetPoints()
        {
            List<Point> res = new List<Point>();

            Dictionary<int, List<ActiveEdge>> edges = _GetEdgesTable(_vertices);
            int yMin = (int)_vertices.Min(v => v.Y);
            int yMax = (int)_vertices.Max(v => v.Y);

            List<ActiveEdge> activeEdges = new List<ActiveEdge>();
            for (int y = yMax; y >= yMin; --y)
            {
                activeEdges.RemoveAll(e => e.YLast == y);
                activeEdges.ForEach(e => e.UpdateX());

                if (edges.ContainsKey(y))
                {
                    activeEdges.AddRange(edges[y]);
                }
                activeEdges.Sort((a, b) => a.CurrentX.CompareTo(b.CurrentX));

                for (int i = 0; i < activeEdges.Count; i += 2)
                {
                    int begin = (int)activeEdges[i].CurrentX;
                    int end = (int)activeEdges[i + 1].CurrentX;

                    res.AddRange(Enumerable.Range(begin, end - begin + 1).Select(x => new Point(x, y)));                        
                }
            }

            return res.ToArray(); ;
        }
        
        private Dictionary<int, List<ActiveEdge>> _GetEdgesTable(Point[] vertices)
        {
            Dictionary<int, List<ActiveEdge>> edges = new Dictionary<int, List<ActiveEdge>>();

            for (int i = 0; i < vertices.Length; ++i)
            {
                Segment edge = new Segment(vertices[i],
                                     vertices[(i + 1) % vertices.Length]);

                _AddEdge(edge, edges);
            }

            return edges;
        }

        private void _AddEdge(Segment edge, Dictionary<int, List<ActiveEdge>> edges)
        {
            if (!edge.IsHorizontal)
            {
                int key = (int)edge.Begin.Y;

                if (!edges.ContainsKey(key))
                {
                    edges.Add(key, new List<ActiveEdge>());
                }

                edges[key].Add(new ActiveEdge(edge));
            }
        }
    }
}

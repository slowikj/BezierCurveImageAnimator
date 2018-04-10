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
    public class PolylineMover
    {
        private const int NO_VERTEX = -1;

        private Polyline _polyline;
        private int _focusedVertexIndex;

        public PolylineMover(Polyline polyline)
        {
            _polyline = polyline;
            _focusedVertexIndex = NO_VERTEX;
        }

        public void SetFocus(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                _focusedVertexIndex = _polyline.GetIndexClickedBy(e.Location);
            }
        }

        public void ResetFocus()
        {
            _focusedVertexIndex = NO_VERTEX;
        }

        public bool MoveVertex(MouseEventArgs e)
        {
            if(_focusedVertexIndex != NO_VERTEX)
            {
                _polyline[_focusedVertexIndex].Location = e.Location;
                return true;
            }

            return false;
        }
    }
}

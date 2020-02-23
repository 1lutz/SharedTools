using System;
using System.Drawing;

namespace SharedTools
{
    public static class GraphicsExtensions
    {
        public static void FillCenteredCircle(this Graphics g, Brush brush, PointF center, float radius)
        {
            float durchmesser = 2 * radius;
            g.FillEllipse(brush, center.X - radius, center.Y - radius, durchmesser, durchmesser);
        }
    }
}

using System.Drawing;

namespace CardonerSistemas
{
    internal static class Drawing
    {
        internal static void Line(System.Drawing.Graphics graphics, int x1, int y1, int x2, int y2, Color color)
        {
            using (Pen pen = new Pen(color))
            {
                graphics.DrawLine(pen, x1, y1, x2, y2);
            }
        }

        internal static void RoundedRectangle(System.Drawing.Graphics graphics, Pen pen, int x, int y, int w, int h, int rx, int ry)
        {
            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            { 
                path.AddArc(x, y, rx + rx, ry + ry, 180, 90);
                path.AddLine(x + rx, y, x + w - rx, y);
                path.AddArc(x + w - 2 * rx, y, 2 * rx, 2 * ry, 270, 90);
                path.AddLine(x + w, y + ry, x + w, y + h - ry);
                path.AddArc(x + w - 2 * rx, y + h - 2 * ry, rx + rx, ry + ry, 0, 91);
                path.AddLine(x + rx, y + h, x + w - rx, y + h);
                path.AddArc(x, y + h - 2 * ry, 2 * rx, 2 * ry, 90, 91);
                path.CloseFigure();
                graphics.DrawPath(pen, path);
            }
        }
    }
}

using System.Drawing;

namespace CardonerSistemas
{
    class Drawing
    {
        static void DrawRoundedRectangle(Graphics g, Pen p, int x, int y, int w, int h, int rx, int ry)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(x, y, rx + rx, ry + ry, 180, 90);
            path.AddLine(x + rx, y, x + w - rx, y);
            path.AddArc(x + w - 2 * rx, y, 2 * rx, 2 * ry, 270, 90);
            path.AddLine(x + w, y + ry, x + w, y + h - ry);
            path.AddArc(x + w - 2 * rx, y + h - 2 * ry, rx + rx, ry + ry, 0, 91);
            path.AddLine(x + rx, y + h, x + w - rx, y + h);
            path.AddArc(x, y + h - 2 * ry, 2 * rx, 2 * ry, 90, 91);
            path.CloseFigure();
            g.DrawPath(p, path);
        }
    }
}

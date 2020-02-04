using System;
using System.Drawing;

namespace CardonerSistemas
{
    static class Graphics
    {
        internal static Icon GetIconFromBitmap(Bitmap bitmap)
        {
            IntPtr pointerIcon = bitmap.GetHicon();
            using (Icon icon = Icon.FromHandle(pointerIcon))
            {
                return icon;
            }
        }

        /// <summary>
        /// Draws a text with a shadow.
        /// ie: RenderDropshadowText(e.Graphics, "Dropshadow Text", this.Font, Color.MidnightBlue, Color.DimGray, 64, new PointF(10, 10));
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="foreground"></param>
        /// <param name="shadow"></param>
        /// <param name="shadowAlpha"></param>
        /// <param name="location"></param>
        internal static void RenderDropshadowText(System.Drawing.Graphics graphics, string text, Font font, Color foreground, Color shadow, int shadowAlpha, PointF location)
        {
            const int DISTANCE = 2;

            for (int offset = 1; 0 <= offset; offset--)
            {
                Color color = ((offset < 1) ?
                    foreground : Color.FromArgb(shadowAlpha, shadow));
                using (var blurBrush = new SolidBrush(Color.FromArgb((shadowAlpha / 2), color)))
                {
                    var point = new PointF()
                    {
                        X = location.X + (offset * DISTANCE),
                        Y = location.Y + (offset * DISTANCE)
                    };
                    graphics.DrawString(text, font, blurBrush, (point.X + 1), point.Y);
                    graphics.DrawString(text, font, blurBrush, (point.X - 1), point.Y);
                }
            }
        }
    }
}
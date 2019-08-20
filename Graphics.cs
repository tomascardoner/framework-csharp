using System;
using System.Drawing;

namespace CardonerSistemas
{
    static class Graphics
    {
        public static Icon GetIconFromBitmap(Bitmap bitmap)
        {
            IntPtr pointerIcon = bitmap.GetHicon();
            using (Icon icon = Icon.FromHandle(pointerIcon))
            {
                return icon;
            }
        }
    }
}
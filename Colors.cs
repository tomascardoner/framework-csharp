using System.Drawing;

namespace CardonerSistemas
{
    class Colors
    {
        static public Color SetColor(Color? newColor, Color defaultColor)
        {
            if (newColor.HasValue)
            {
                return newColor.Value;
            }
            else
            {
                return defaultColor;
            }
        }
    }
}

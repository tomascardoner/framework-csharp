using System;
using System.Drawing;
using System.Windows.Forms;

namespace CardonerSistemas
{
    static class Form
    {
        static public void CenterOnScreen(ref Form theForm)
        {
            // Remember to set StartPosition = Manual in the properties window.
            Rectangle screenSize = Screen.PrimaryScreen.WorkingArea;
            theForm.Location = new Point((screenSize.Width - theForm.Width) / 2, (screenSize.Height - theForm.Height) / 2);
        }

        static public void SetOnRightSideOfScreen(ref Form theForm)
        {
            // Remember to set StartPosition = Manual in the properties window.
            Rectangle screenSize = Screen.PrimaryScreen.WorkingArea;
            theForm.Location = new Point((screenSize.Width - theForm.Width), (screenSize.Height - theForm.Height) / 2);
        }

        static public void FitToScreen(ref Form theForm)
        {
            Rectangle screenSize = Screen.PrimaryScreen.WorkingArea;
            theForm.Location = new Point(0, 0);
            theForm.Size = new Size(screenSize.Width, screenSize.Height);
        }

        static public void FitHeightToScreen(ref Form theForm)
        {
            Rectangle screenSize = Screen.PrimaryScreen.WorkingArea;
            theForm.Location = new Point(theForm.Left, 0);
            theForm.Size = new Size(theForm.Width, screenSize.Height);
        }
    }
}

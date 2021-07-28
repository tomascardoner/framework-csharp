using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CardonerSistemas
{
    static class Forms
    {

        #region Declarations

        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        internal const int LabelAndControlSeparation = 6;
        internal const int WindowMargin = 12;

        #endregion

        #region Size and Position

        static public void CenterOnScreen(Form theForm)
        {
            // Remember to set StartPosition = Manual in the properties window.
            Rectangle screenSize = Screen.PrimaryScreen.WorkingArea;
            theForm.Location = new Point((screenSize.Width - theForm.Width) / 2, (screenSize.Height - theForm.Height) / 2);
        }

        static public void SetOnRightSideOfScreen(Form theForm)
        {
            // Remember to set StartPosition = Manual in the properties window.
            Rectangle screenSize = Screen.PrimaryScreen.WorkingArea;
            theForm.Location = new Point((screenSize.Width - theForm.Width), (screenSize.Height - theForm.Height) / 2);
        }

        static public void FitToScreen(Form theForm)
        {
            Rectangle screenSize = Screen.PrimaryScreen.WorkingArea;
            theForm.Location = new Point(0, 0);
            theForm.Size = new Size(screenSize.Width, screenSize.Height);
        }

        static public void FitHeightToScreen(Form theForm)
        {
            Rectangle screenSize = Screen.PrimaryScreen.WorkingArea;
            theForm.Location = new Point(theForm.Left, 0);
            theForm.Size = new Size(theForm.Width, screenSize.Height);
        }

        static public void CenterToParent(Form parentForm, Form childForm)
        {
            int parentFormTop;
            int parentFormLeft;

            if (parentForm.MdiParent == null | childForm.MdiParent != null)
            {
                parentFormTop = parentForm.Top;
                parentFormLeft = parentForm.Left;
            }
            else
            {

                //TODO: Need to take account of the scaling when different to 1

                // Gets the scaling factor of the screen
                System.Drawing.Graphics g = parentForm.CreateGraphics();
                float scalingFactor = (int)g.DpiX / 96;

                // Parent form title bar height and border size calculation
                int borderSize = (parentForm.Width - parentForm.ClientSize.Width) / 2;
                int titleSize = (parentForm.Height - parentForm.ClientSize.Height) - (borderSize * 2);

                // Gets parent form's absolute position
                Point point = parentForm.PointToScreen(new Point(0, 0));
                parentFormTop = point.Y - borderSize - titleSize;
                parentFormLeft = point.X - borderSize;
            }
            childForm.Top = parentFormTop  + (int)((parentForm.Height - childForm.Height) / 2);
            childForm.Left = parentFormLeft + (int)((parentForm.Width - childForm.Width) / 2);
        }

        #endregion

        #region Instances

        static public bool IsLoaded(Form[] forms, string formName)
        {
            foreach (Form form in forms)
            {
                if (form.Name == formName)
                {
                    return true;
                }
            }
            return false;
        }

        static public bool IsLoaded(string formName)
        {
            return IsLoaded(Application.OpenForms.Cast<Form>().ToArray(), formName);
        }

        static public bool IsLoaded(Form[] forms, string formName, string formText)
        {
            foreach (Form form in forms)
            {
                if (form.Name == formName && form.Text == formText)
                {
                    return true;
                }
            }
            return false;
        }

        static public bool IsLoaded(string formName, string formText)
        {
            return IsLoaded(Application.OpenForms.Cast<Form>().ToArray(), formName, formText);
        }

        static public Form GetInstance(Form[] forms, string formClassName)
        {
            foreach (Form form in forms)
            {
                if (form.Name == formClassName)
                {
                    return form;
                }
            }
            return null;
        }

        static public Form GetInstance(string formClassName)
        {
            return GetInstance(Application.OpenForms.Cast<Form>().ToArray(), formClassName);
        }

        static public Form GetInstance(Form[] forms, string formClassName, string formText)
        {
            foreach (Form form in forms)
            {
                if (form.Name == formClassName && form.Text == formText)
                {
                    return form;
                }
            }
            return null;
        }

        static public Form GetInstance(string formClassName, string formText)
        {
            return GetInstance(Application.OpenForms.Cast<Form>().ToArray(), formClassName, formText);
        }

        static public void CloseAll(Form[] forms, params string[] exceptForms)
        {
            foreach (Form form in forms)
            {
                bool exceptThis = false;

                foreach (string exceptForm in exceptForms)
                {
                    if (form.Name == exceptForm)
                    {
                        exceptThis = true;
                        break;
                    }
                }

                if (!exceptThis)
                {
                    form.Close();
                    form.Dispose();
                }
            }
        }

        static public void CloseAll(params string[] exceptForms)
        {
            CloseAll(Application.OpenForms.Cast<Form>().ToArray(), exceptForms);
        }

        #endregion

        #region Find Window and active it

        internal static IntPtr FindWindow(string caption)
        {
            return FindWindow(System.String.Empty, caption);
        }

        internal static void ActivateApp(string processName)
        {
            //Process[] p = Process.GetProcessesByName(processName);

            //// Activate the first application we find with this name
            //if (p.Count() > 0)
            //    SetForegroundWindow(p[0].MainWindowHandle);
        }

        #endregion

        #region Mdi Childs

        static public void MdiChildShow(Form MdiForm, Form childForm, bool centerForm)
        {
            MdiForm.Cursor = Cursors.WaitCursor;

            childForm.MdiParent = MdiForm;
            if (centerForm)
            {
                CenterToParent(MdiForm, childForm);
            }
            else
            {
                MdiChildPositionAndSizeToFit(MdiForm, childForm);
            }

            childForm.Show();
            if (childForm.WindowState == FormWindowState.Minimized)
            {
                childForm.WindowState = FormWindowState.Normal;
            }

            childForm.Focus();

            MdiForm.Cursor = Cursors.Default;
        }

        static public void MdiChildPositionAndSizeToFit(Form mdiForm, Form childForm)
        {
            childForm.SuspendLayout();
            childForm.MdiParent = mdiForm;
            if (childForm.WindowState != FormWindowState.Normal)
            {
                childForm.WindowState = FormWindowState.Normal;
            }
            childForm.Dock = DockStyle.Fill;
            childForm.ResumeLayout(true);
        }

        static public void MdiChildCenterToClientArea(Form childForm, System.Drawing.Size MdiClientAreaSize)
        {
            if (childForm.WindowState != FormWindowState.Normal)
            {
                childForm.WindowState = FormWindowState.Normal;
            }
            childForm.Top = (int)((MdiClientAreaSize.Height - childForm.Height) / 2);
            childForm.Left = (int)((MdiClientAreaSize.Width - childForm.Width) / 2);
        }

        static public bool MdiChildIsLoaded(Form MdiForm, string formName)
        {
            return IsLoaded(MdiForm.MdiChildren, formName);
        }

        static public bool MdiChildIsLoaded(Form MdiForm, string formName, string formText)
        {
            return IsLoaded(MdiForm.MdiChildren, formName, formText);
        }

        static public Form MdiChildGetInstance(Form MdiForm, string formClassName)
        {
            return GetInstance(MdiForm.MdiChildren, formClassName);
        }

        static public Form MdiChildGetInstance(Form MdiForm, string formName, string formText)
        {
            return GetInstance(MdiForm.MdiChildren, formName, formText);
        }

        static public void MdiChildCloseAll(Form MdiForm, params string[] exceptForms)
        {
            CloseAll(MdiForm.MdiChildren, exceptForms);
        }

        #endregion

        #region Controls change state

        static public void ControlsChangeStateEnabled(Control.ControlCollection controlsContainer, bool valueState, bool applyToLabels, bool applyToPanels, bool recursive, params string[] exceptControls)
        {
            foreach (Control control in controlsContainer)
            {
                bool exceptThis = false;

                foreach (string exceptControl in exceptControls)
                {
                    if (control.Name == exceptControl)
                    {
                        exceptThis = true;
                    }
                }

                if (!exceptThis)
                {
                    if (recursive && control.HasChildren)
                    {
                        ControlsChangeStateEnabled(control.Controls, valueState, applyToLabels, applyToPanels, recursive, exceptControls);
                    }

                    if (control is Label)
                    {
                        if (applyToLabels)
                        {
                            control.Enabled = valueState;
                        }
                    }
                    else if (control is Panel)
                    {
                        if (applyToPanels)
                        {
                            control.Enabled = valueState;
                        }
                    }
                    else if (control is Button)
                    {
                        if (applyToPanels)
                        {
                            control.Enabled = valueState;
                        }
                    }
                    else
                    {
                        control.Enabled = valueState;
                    }
                }
            }
        }

        static public void ControlsChangeStateReadOnly(Control.ControlCollection controlsContainer, bool valueState, bool recursive, params string[] exceptControls)
        {
            foreach (Control control in controlsContainer)
            {
                bool exceptThis = false;

                foreach (string exceptControl in exceptControls)
                {
                    if (control.Name == exceptControl)
                    {
                        exceptThis = true;
                    }
                }

                if (!exceptThis)
                {
                    if (recursive && control.HasChildren)
                    {
                        ControlsChangeStateReadOnly(control.Controls, valueState, recursive, exceptControls);
                    }

                    if (control is TextBox)
                    {
                        TextBox textBox = (TextBox)control;
                        textBox.ReadOnly = valueState;
                    }
                    else if (control is MaskedTextBox)
                    {
                        MaskedTextBox textBox = (MaskedTextBox)control;
                        textBox.ReadOnly = valueState;
                    }
                    else if (control is System.Windows.Forms.ComboBox)
                    {
                        control.Enabled = !valueState;
                    }
                    else if (control is CheckBox)
                    {
                        control.Enabled = !valueState;
                    }
                    else if (control is DateTimePicker)
                    {
                        control.Enabled = !valueState;
                    }
                    else if (control is Button)
                    {
                        control.Enabled = !valueState;
                    }
                    else if (control is ToolStrip)
                    {
                        control.Enabled = !valueState;
                    }
                }
            }
        }

        #endregion

        #region Appearance

        internal static void SetFont(Form form, Font font)
        {
            form.Font = font;

            SetFontOfControls(form.Controls, font);
        }

        private static void SetFontOfControls(Control.ControlCollection controls, Font font)
        {
            foreach (Control control in controls)
            {
                if (control is ToolStrip)
                {
                    control.Font = font;
                    foreach (ToolStripItem item in ((ToolStrip)control).Items)
                    {
                        if (item is ToolStripComboBox | item is ToolStripTextBox)
                        {
                            item.Font = font;
                        }
                        else if (item is ToolStripControlHost)
                        {
                            if (((ToolStripControlHost)item).Control is DateTimePicker)
                            {
                                ((DateTimePicker)((ToolStripControlHost)item).Control).CalendarFont = font;
                            }
                        }
                    }
                }
                else if (control is FlowLayoutPanel)
                {
                    SetFontOfControls(control.Controls, font);
                }
            }
        }

        #endregion

        #region Input Box

        static private Form CreateInputBoxForm(string title, string prompt, int width)
        {
            Form inputBox = new Form();
            Size size = new Size(width, 70);

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = title;

            TextBox textBox = new TextBox();
            textBox.Size = new Size(size.Width - 10, 23);
            textBox.Location = new Point(5, 5);
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "buttonOk";
            okButton.Size = new Size(75, 23);
            okButton.Text = "&Aceptar";
            okButton.Location = new Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "buttonCancel";
            cancelButton.Size = new Size(75, 23);
            cancelButton.Text = "&Cancelar";
            cancelButton.Location = new Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            return inputBox;
        }

        static internal DialogResult InputBoxString(string title, string prompt, bool allowEmpty, int maxLenght, ref string value)
        {
            if (maxLenght < 1)
            {
                return DialogResult.Cancel;
            }

            Form inputBox = CreateInputBoxForm(title, prompt, 200);


            return inputBox.ShowDialog();
        }

        #endregion

    }
}

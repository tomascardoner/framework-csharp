using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CardonerSistemas
{
    static class Forms
    {

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
            childForm.Top = parentForm.Top + (int)((parentForm.Height - childForm.Height) / 2);
            childForm.Left = parentForm.Left + (int)((parentForm.Width - childForm.Width) / 2);
        }

        #endregion

        #region Instances

        static public bool IsLoaded(FormCollection forms, string formName)
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
            return IsLoaded(Application.OpenForms, formName);
        }

        static public bool IsLoaded(FormCollection forms, string formName, string formText)
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
            return IsLoaded(Application.OpenForms, formName, formText);
        }

        static public Form GetInstance(FormCollection forms, string formName)
        {
            foreach (Form form in forms)
            {
                if (form.Name == formName)
                {
                    return form;
                }
            }
            return null;
        }

        static public Form GetInstance(string formName)
        {
            return GetInstance(Application.OpenForms, formName);
        }

        static public Form GetInstance(FormCollection forms, string formName, string formText)
        {
            foreach (Form form in forms)
            {
                if (form.Name == formName && form.Text == formText)
                {
                    return form;
                }
            }
            return null;
        }

        static public Form GetInstance(string formName, string formText)
        {
            return GetInstance(Application.OpenForms, formName);
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

        #region MDI Childs

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

        static public void MdiChildPositionAndSizeToFit(Form MdiForm, Form childForm)
        {
            childForm.SuspendLayout();
            childForm.MdiParent = MdiForm;
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
            FormCollection forms = (FormCollection)MdiForm.MdiChildren.GetEnumerator();
            return IsLoaded(forms, formName);
        }

        static public bool MdiChildIsLoaded(Form MdiForm, string formName, string formText)
        {
            FormCollection forms = (FormCollection)MdiForm.MdiChildren.GetEnumerator();
            return IsLoaded(forms, formName, formText);
        }

        static public Form MdiChildGetInstance(Form MdiForm, string formName)
        {
            FormCollection forms = (FormCollection)MdiForm.MdiChildren.GetEnumerator();
            return GetInstance(forms, formName);
        }

        static public Form MdiChildGetInstance(Form MdiForm, string formName, string formText)
        {
            FormCollection forms = (FormCollection)MdiForm.MdiChildren.GetEnumerator();
            return GetInstance(forms, formName, formText);
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
                    else if (control is ComboBox)
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

    }
}

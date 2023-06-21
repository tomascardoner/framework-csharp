using System.Collections.Generic;
using System.Windows.Forms;

namespace CardonerSistemas
{
    internal class TabControlExtension
    {

        #region Declarations

        private readonly List<string> tabPagesOrder;
        private readonly List<TabPage> tabPagesHidden;

        #endregion

        public TabControlExtension(TabControl tabControl)
        {
            tabPagesOrder = new List<string>();
            tabPagesHidden = new List<TabPage>();
            foreach (TabPage tabPageCurrent in tabControl.TabPages)
            {
                tabPagesOrder.Add(tabPageCurrent.Name);
            }
        }

        internal void HidePage(TabControl tabControl, TabPage tabPage)
        {
            if (tabControl.TabPages.Contains(tabPage))
            {
                tabPagesHidden.Add(tabPage);
                tabControl.TabPages.Remove(tabPage);
            }
        }

        internal void ShowPage(TabControl tabControl, TabPage tabPage)
        {
            if (tabPagesHidden.Contains(tabPage))
            {
                tabControl.TabPages.Insert(GetTabPageInsertionPoint(tabControl, tabPage.Name), tabPage);
                tabPagesHidden.Remove(tabPage);
            }
        }

        internal void PageVisible(TabControl tabControl, TabPage tabPage, bool value)
        {
            if (value)
            {
                ShowPage(tabControl, tabPage);
            }
            else
            {
                HidePage(tabControl, tabPage);
            }
        }

        private int GetTabPageInsertionPoint(TabControl tabControl, string tabPageName)
        {
            int tabPageIndex;
            TabPage tabPageCurrent;
            int tabNameIndex;
            string tabNameCurrent;

            for (tabPageIndex = 0; tabPageIndex < tabControl.TabPages.Count; tabPageIndex++)
            {
                tabPageCurrent = tabControl.TabPages[tabPageIndex];
                for (tabNameIndex = tabPageIndex; tabNameIndex < tabPagesOrder.Count; tabNameIndex++)
                {
                    tabNameCurrent = tabPagesOrder[tabNameIndex];
                    if (tabNameCurrent == tabPageCurrent.Name)
                    {
                        break;
                    }
                    if (tabNameCurrent == tabPageName)
                    {
                        return tabPageIndex;
                    }
                }
            }
            return tabPageIndex;
        }
    }
}

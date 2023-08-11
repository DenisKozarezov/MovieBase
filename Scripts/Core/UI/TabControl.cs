using System;
using System.Windows.Forms;

namespace Core.UI
{
    class TabControl : BaseUIElement
    {
        public string SelectedItem { private set; get; }
        public int SelectedIndex { private set; get; }
        public bool ShowTabes { private set; get; }

        public event Action<string> SelectedItemChanged;

        public TabControl(System.Windows.Forms.TabControl control, bool showTabes) : base(control)
        {
            ShowTabes = showTabes;
            if (!ShowTabes)
            {               
                control.Appearance = TabAppearance.FlatButtons;
                control.ItemSize = new System.Drawing.Size(0, 1);
                control.SizeMode = TabSizeMode.Fixed;
            }
        }

        /// <summary>
        /// Открывает вкладку по индексу.
        /// </summary>
        public void OpenTab(int index)
        {
            System.Windows.Forms.TabControl tabControl = (System.Windows.Forms.TabControl)Control;
            if (index <= tabControl.TabCount - 1)
            {
                SelectedIndex = index;
                tabControl.SelectedIndex = index;
                SelectedItemChanged.Invoke(tabControl.SelectedTab.Text);
            }
        }

        /// <summary>
        /// Открывает вкладку по наименованию.
        /// </summary>
        /// <param name="name"></param>
        public void OpenTab(string name)
        {
            System.Windows.Forms.TabControl tabControl = (System.Windows.Forms.TabControl)Control;
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                if (tabControl.TabPages[i].Text == name)
                {
                    SelectedIndex = i;
                    SelectedItem = name;
                    tabControl.SelectedIndex = i;
                    SelectedItemChanged?.Invoke(name);
                    break;
                }
            }
        }

        public void HideTab(string name)
        {
            System.Windows.Forms.TabControl tabControl = (System.Windows.Forms.TabControl)Control;
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                if (tabControl.TabPages[i].Text == name)
                {
                    tabControl.TabPages[i].Size = new System.Drawing.Size(0, 1);
                    break;
                }
            }
          
        }
    }
}
using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace Core.UI.Forms
{
    static class Help
    {
        private static readonly string HelpURL = $"{Directory.GetCurrentDirectory()}\\HelpSmith.chm";
        public static readonly CancelEventHandler OnHelpButtonClickHandler = new CancelEventHandler(OnHelpButtonClicked);

        private static void ShowHelp()
        {
            try
            {
                System.Windows.Forms.Help.ShowHelp(null, HelpURL);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Справка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void OnHelpButtonClicked(object sender, CancelEventArgs e)
        {
            ShowHelp();
            e.Cancel = true;
        }
    }
}
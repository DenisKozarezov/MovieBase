using System;
using System.Windows.Forms;

namespace Core
{
    static class Program
    {
        private static ApplicationContext Context { set; get; }

        public static Form CurrentForm { set; get; }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Context = new ApplicationContext(new UI.Forms.Authorization());
            Application.Run(Context);
        }

        /// <summary>
        /// Открывает указанную форму.
        /// </summary>
        public static void OpenForm(Form form)
        {
            CurrentForm = form;
            Context.MainForm = form;
            Context.MainForm.Show();
        }
        /// <summary>
        /// Закрывает указанную форму. Прекращает работу программы, если 
        /// указанная форма является основной.
        /// </summary>
        public static void CloseForm(Form form)
        {
            if (Context.MainForm == form) Context.ExitThread();
            else form.Close();
        }
    }
}

using PluralsightWinFormsDemoApp.Presenter;
using PluralsightWinFormsDemoApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm myMainForm = new MainForm();
            MainPresenter myMainPresenter = new MainPresenter(myMainForm);
            Application.Run(myMainForm);
        }
    }
}

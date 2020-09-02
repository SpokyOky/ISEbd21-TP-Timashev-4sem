using SecurityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecuritySystemClientView
{
    static class Program
    {
        public static ClientViewModel Client { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            APIClient.Connect();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new FormEnter();
            form.ShowDialog();

            if (Client != null)
            {
                Application.Run(new FormMain());
            }
        }
    }
}

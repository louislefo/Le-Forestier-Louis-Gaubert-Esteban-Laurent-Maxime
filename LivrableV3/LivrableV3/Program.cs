using System;
using System.Windows.Forms;

namespace LivrableV3
{
    static class Program
    {
        /// point d entree principal de l application
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

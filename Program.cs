using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Valentine.x16;

namespace Valentine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}

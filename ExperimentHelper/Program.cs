using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExperimentHelper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (RunningInstance() == null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("有一个和本程序相同的应用程序已经在运行, 请不要同时运行多个本程序.\n\n这个程序即将退出。", 
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        static System.Diagnostics.Process RunningInstance()
        {
            System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(current.ProcessName);
            foreach (System.Diagnostics.Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", @"/") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
    }

}

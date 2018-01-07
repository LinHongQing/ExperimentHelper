using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Util;
using System;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_FindWindow : IProcessItem
    {
        private WindowHandle instance;
        private string windowName;

        public ProcessItem_FindWindow(WindowHandle instance, string windowName)
        {
            this.instance = instance;
            this.windowName = windowName;
        }

        public class NoSpecifiedWindowException : ProcessException
        {
            public NoSpecifiedWindowException() : base() { }
        }

        public ResultItem Execute()
        {
            IntPtr foundWindowHandle = Win32.FindWindow(null, windowName);
            if (foundWindowHandle == IntPtr.Zero)
                throw new NoSpecifiedWindowException();
            instance.SetCurrentHandle(foundWindowHandle);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("成功找到名称为 {0} 的窗口句柄 0x{1:x8}", windowName, foundWindowHandle.ToInt32());
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}

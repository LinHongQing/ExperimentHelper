using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Util;
using System;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_FindWindowEx : IProcessItem
    {
        private WindowHandle instance;
        private string windowName;

        public ProcessItem_FindWindowEx(WindowHandle instance, string windowName)
        {
            this.instance = instance;
            this.windowName = windowName;
        }

        public class NoSpecifiedWindowException : ProcessException
        {
            public NoSpecifiedWindowException() : base() { }
        }

        public class InvalidParentHandleException : ProcessException
        {
            public InvalidParentHandleException() : base() { }
        }

        public ResultItem Execute()
        {
            IntPtr parentHandle = instance.GetParentHandle();
            if (parentHandle == IntPtr.Zero)
                throw new InvalidParentHandleException();
            IntPtr foundWndHandle = Win32.FindWindowEx(parentHandle, IntPtr.Zero, null, windowName);
            if (foundWndHandle == IntPtr.Zero)
                throw new NoSpecifiedWindowException();
            instance.SetCurrentHandle(foundWndHandle);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("成功在父级窗口句柄 0x{0:x8} 中找到 {1} 的句柄 0x{2:x8}",
                parentHandle,
                windowName,
                foundWndHandle.ToInt32());
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}

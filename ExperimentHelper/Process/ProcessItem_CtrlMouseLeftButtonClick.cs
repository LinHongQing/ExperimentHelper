using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Util;
using System;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_CtrlMouseLeftButtonClick : IProcessItem
    {
        private WindowHandle instance;

        public ProcessItem_CtrlMouseLeftButtonClick(WindowHandle instance)
        {
            this.instance = instance;
        }

        public class InvalidCurrentHandleException : ProcessException
        {
            public InvalidCurrentHandleException() : base() { }
        }

        public ResultItem Execute()
        {
            IntPtr currentHandle = instance.GetCurrentHandle();
            if (currentHandle == IntPtr.Zero)
                throw new InvalidCurrentHandleException();
            Win32.PostMessage(currentHandle, (int)Win32.MessageFlags.WM_LBUTTONDOWN, null, null);
            Win32.PostMessage(currentHandle, (int)Win32.MessageFlags.WM_LBUTTONDOWN, null, null);
            Win32.PostMessage(currentHandle, (int)Win32.MessageFlags.WM_LBUTTONUP, null, null);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("发送给句柄 0x{0:x8} 鼠标点击事件完成", currentHandle.ToInt32());
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}

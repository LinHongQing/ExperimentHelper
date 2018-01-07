using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Util;
using System;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_SetTextBoxValue : IProcessItem
    {
        private WindowHandle instance;
        private string textValue;

        public ProcessItem_SetTextBoxValue(WindowHandle instance, string textValue)
        {
            this.instance = instance;
            this.textValue = textValue;
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
            Win32.SendMessage(currentHandle, (int)Win32.MessageFlags.WM_SETTEXT, null, textValue);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("已将句柄 0x{0:x8} 的 TextBox 内容设置为 \"{1}\"", currentHandle.ToInt32(), textValue);
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}

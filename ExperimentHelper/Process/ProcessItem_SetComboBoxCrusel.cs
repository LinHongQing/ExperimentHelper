using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Util;
using System;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_SetComboBoxCrusel : IProcessItem
    {
        private WindowHandle instance;
        private int curselIndex;

        public ProcessItem_SetComboBoxCrusel(WindowHandle instance, int curselIndex)
        {
            this.instance = instance;
            this.curselIndex = curselIndex;
        }

        public class InvalidCurrentHandleException : ProcessException
        {
            public InvalidCurrentHandleException() : base () { }
        }

        public ResultItem Execute()
        {
            IntPtr currentHandle = instance.GetCurrentHandle();
            if (currentHandle == IntPtr.Zero)
                throw new InvalidCurrentHandleException();
            Win32.SendMessage(currentHandle, (int)Win32.MessageFlags.CB_SETCURSEL, curselIndex, 0);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("已将句柄 0x{0:x8} 的 ComboBox 选择索引设置为第 {1} 项", currentHandle.ToInt32(), curselIndex + 1);
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}

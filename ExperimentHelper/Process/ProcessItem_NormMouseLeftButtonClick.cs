using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Util;
using System;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_NormMouseLeftButtonClick : IProcessItem
    {
        public ResultItem Execute()
        {
            Win32.mouse_event((int)Win32.MouseEventFlags.LeftDown | (int)Win32.MouseEventFlags.LeftUp, 0, 0, 0, UIntPtr.Zero);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("鼠标点击完成");
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}

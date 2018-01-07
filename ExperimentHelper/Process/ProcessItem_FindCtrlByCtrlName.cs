using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_FindCtrlByCtrlName : IProcessItem
    {

        private WindowHandle instance;
        private string ctrlName;
        private int selectedIndex;
        private List<IntPtr> matchedWindowsHandle;

        public ProcessItem_FindCtrlByCtrlName(WindowHandle instance, string ctrlName, int selectedIndex)
        {
            this.instance = instance;
            this.ctrlName = ctrlName;
            this.selectedIndex = selectedIndex;
        }

        public class InvalidParentHandleException : ProcessException
        {
            public InvalidParentHandleException() : base() { }
        }

        public class NoSelectedNameOrIndexWindowException : ProcessException
        {
            public NoSelectedNameOrIndexWindowException() : base() { }
        }

        private bool Filter(IntPtr handle, string ctrlName)
        {
            StringBuilder title = new StringBuilder(100);
            Win32.GetWindowText(handle, title, 100);// 取标题
            if (title.ToString().Contains(ctrlName))// 筛选
            {
                matchedWindowsHandle.Add(handle);
            }
            return true;
        }

        public ResultItem Execute()
        {
            IntPtr parentHandle = instance.GetParentHandle();
            if (parentHandle == IntPtr.Zero)
                throw new InvalidParentHandleException();
            matchedWindowsHandle = new List<IntPtr>();
            Win32.EnumChildWindows(parentHandle, Filter, ctrlName);
            if (matchedWindowsHandle.Count <= 0 || matchedWindowsHandle.Count <= selectedIndex)
                throw new NoSelectedNameOrIndexWindowException();
            instance.SetCurrentHandle(matchedWindowsHandle[selectedIndex]);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("成功找到指定控件 \"{0}\" 的句柄，共 {1} 个, 当前选择第 {2} 个 0x{3:x8}",
                ctrlName, matchedWindowsHandle.Count,
                selectedIndex + 1,
                matchedWindowsHandle[selectedIndex].ToInt32());
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}

using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_FindWindowByName : IProcessItem
    {
        private WindowHandle instance;
        private string titleName;
        private int selectedIndex;
        private List<IntPtr> matchedWindowsHandle;
        private bool needActive;

        public ProcessItem_FindWindowByName(WindowHandle instance, string titleName, int selectedIndex, bool needActive)
        {
            this.instance = instance;
            this.titleName = titleName;
            this.selectedIndex = selectedIndex;
            this.needActive = needActive;
        }

        public class NoSelectedNameOrIndexWindowException : ProcessException
        {
            public NoSelectedNameOrIndexWindowException() : base () { }
        }

        private bool Filter(IntPtr handle, string titleName)
        {
            StringBuilder title = new StringBuilder(100);
            Win32.GetWindowText(handle, title, 100);// 取标题
            if (title.ToString().Contains(titleName))// 筛选
            {
                if (needActive)
                {
                    if (Win32.GetForegroundWindow() == handle)
                    {
                        matchedWindowsHandle.Add(handle);
                    }
                }
                else
                {
                    matchedWindowsHandle.Add(handle);
                }
            }
            return true;
        }

        public ResultItem Execute()
        {
            matchedWindowsHandle = new List<IntPtr>();
            Win32.EnumWindow filter = new Win32.EnumWindow(Filter);
            Win32.EnumWindows(filter, titleName);
            if (matchedWindowsHandle.Count <= 0 || matchedWindowsHandle.Count <= selectedIndex)
                throw new NoSelectedNameOrIndexWindowException();
            instance.SetCurrentHandle(matchedWindowsHandle[selectedIndex]);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("成功找到指定窗口 \"{0}\" 的句柄，共 {1} 个, 当前选择第 {2} 个 0x{3:x8}",
                titleName,
                matchedWindowsHandle.Count,
                selectedIndex + 1,
                matchedWindowsHandle[selectedIndex].ToInt32());
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}

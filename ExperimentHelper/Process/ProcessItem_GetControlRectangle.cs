using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Util;
using System;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_GetControlRectangle : IProcessItem
    {
        private WindowHandle instance;

        public ProcessItem_GetControlRectangle(WindowHandle instance)
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
            StructComponent.RECT rect = new StructComponent.RECT();
            Win32.GetClientRect(currentHandle, out rect);// 取得窗口内控件的坐标
#if DEBUG
            Console.WriteLine("获取到指定句柄变换前的矩形: " + rect.ToString());
#endif
            StructComponent.POINT point = new StructComponent.POINT
            {
                X = rect.Left,
                Y = rect.Bottom
            };
            Win32.ClientToScreen(currentHandle, ref point);// 取得该控件位置对应的桌面坐标
            rect.Left = point.X;
            rect.Bottom = point.Y;
            point.X = rect.Right;
            point.Y = rect.Top;
            Win32.ClientToScreen(currentHandle, ref point);// 取得该控件位置对应的桌面坐标
            rect.Right = point.X;
            rect.Top = point.Y;
            TargetRectangle targetRectangle = TargetRectangle.GetInstance();
            targetRectangle.SetRectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("获取到指定句柄的矩形: " + rect.ToString());
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}

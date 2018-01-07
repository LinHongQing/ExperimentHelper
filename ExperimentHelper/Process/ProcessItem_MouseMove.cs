using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using ExperimentHelper.Util;
using System;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_MouseMove : IProcessItem
    {
        private int positionX;
        private int positionY;
        public ProcessItem_MouseMove(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
        }
        public ResultItem Execute()
        {
            Win32.SetCursorPos(positionX, positionY);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("鼠标已移到指定位置 ({0}, {1})", positionX, positionY);
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}

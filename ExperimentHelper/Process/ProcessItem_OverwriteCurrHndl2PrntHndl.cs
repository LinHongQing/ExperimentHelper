using ExperimentHelper.Basic;
using ExperimentHelper.Interface;
using System;
using System.Text;

namespace ExperimentHelper.Process
{
    public class ProcessItem_OverwriteCurrHndl2PrntHndl : IProcessItem
    {
        public WindowHandle instance;

        public ProcessItem_OverwriteCurrHndl2PrntHndl(WindowHandle instance)
        {
            this.instance = instance;
        }
        public ResultItem Execute()
        {
            instance.SetParentHandle(instance.GetCurrentHandle());
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("已将当前句柄覆盖到父句柄中, 父句柄为 0x{0:x8}", instance.GetCurrentHandle().ToInt32());
#if DEBUG
            Console.WriteLine(sb.ToString());
#endif
            return new ResultItem(ResultItem.States.OK, sb.ToString());
        }
    }
}

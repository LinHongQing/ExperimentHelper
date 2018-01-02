using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper
{
    class WndItem
    {
        private IntPtr parentWnd;
        private IntPtr currentWnd;

        public IntPtr ParentWnd { get => parentWnd; set => parentWnd = value; }
        public IntPtr CurrentWnd { get => currentWnd; set => currentWnd = value; }

        public WndItem(IntPtr parentWnd, IntPtr currentWnd)
        {
            this.parentWnd = parentWnd;
            this.currentWnd = currentWnd;
        }

        public override string ToString()
        {
            return String.Format("parent: 0x{0:x8}, current: 0x{1:x8}", parentWnd.ToInt32(), currentWnd.ToInt32());
        }
    }
}

using ExperimentHelper.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Basic
{
    public class WindowHandle : IWindowHandleObserverable
    {
        private static WindowHandle _uniqueWndItemInstance;
        private IntPtr parentHandle;
        private IntPtr currentHandle;
        private List<IWindowHandleObserver> observerList;

        private WindowHandle()
        {
            parentHandle = IntPtr.Zero;
            currentHandle = IntPtr.Zero;
            observerList = new List<IWindowHandleObserver>();
        }

        public static WindowHandle GetInstance()
        {
            if (_uniqueWndItemInstance == null)
            {
                _uniqueWndItemInstance = new WindowHandle(); 
            }
            return _uniqueWndItemInstance;
        }

        public IntPtr GetParentHandle()
        {
            return _uniqueWndItemInstance.parentHandle;
        }

        public IntPtr GetCurrentHandle()
        {
            if (_uniqueWndItemInstance == null)
                _uniqueWndItemInstance = new WindowHandle();
            return _uniqueWndItemInstance.currentHandle;
        }

        public void SetParentHandle(IntPtr handle)
        {
            parentHandle = handle;
            NotifyAllWindowHandleObservers();
        }

        public void SetCurrentHandle(IntPtr handle)
        {
            currentHandle = handle;
            NotifyAllWindowHandleObservers();
        }

        public override string ToString()
        {
            return String.Format("parent: 0x{0:x8}, current: 0x{1:x8}", GetParentHandle().ToInt32(), GetCurrentHandle().ToInt32());
        }

        public void RegisterWindowHandleObserver(IWindowHandleObserver observer)
        {
            observerList.Add(observer);
        }

        public void RemoveWindowHandleObserver(IWindowHandleObserver observer)
        {
            observerList.Remove(observer);
        }

        public void NotifyAllWindowHandleObservers()
        {
            for (int i = 0; i < observerList.Count; i++)
            {
                IWindowHandleObserver observer = observerList[i];
                observer.UpdateWindowHandle(_uniqueWndItemInstance);
            }
        }
    }
}

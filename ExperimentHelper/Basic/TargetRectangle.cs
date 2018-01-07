using ExperimentHelper.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Basic
{
    public class TargetRectangle : ITargetRectangleObserable
    {
        private uint left;
        private uint top;
        private uint right;
        private uint bottom;
        private static TargetRectangle _uniqueRectangle;
        private List<ITargetRectangleObserver> observers = new List<ITargetRectangleObserver>();

        private TargetRectangle()
        {
            left = top = right = bottom = 0;
        }

        public static TargetRectangle GetInstance()
        {
            if (_uniqueRectangle == null)
            {
                _uniqueRectangle = new TargetRectangle();
            }
            return _uniqueRectangle;
        }

        public uint GetLeft()
        {
            return left;
        }

        public uint GetTop()
        {
            return top;
        }

        public uint GetRight()
        {
            return right;
        }

        public uint GetBottom()
        {
            return bottom;
        }

        public void SetRectangle(uint left, uint top, uint right, uint bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            NotifyAllTargetRectangleObservers();
        }

        public void RegisterTargetRectangleObserver(ITargetRectangleObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveTargetRectangleObserver(ITargetRectangleObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyAllTargetRectangleObservers()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                ITargetRectangleObserver observer = observers[i];
                observer.UpdateTargetRectangle(_uniqueRectangle);
            }
        }
    }
}

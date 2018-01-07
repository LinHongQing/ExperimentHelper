using ExperimentHelper.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Interface
{
    public interface ITargetRectangleObserable
    {
        void RegisterTargetRectangleObserver(ITargetRectangleObserver observer);
        void RemoveTargetRectangleObserver(ITargetRectangleObserver observer);
        void NotifyAllTargetRectangleObservers();
    }
}

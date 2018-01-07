using ExperimentHelper.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Interface
{
    public interface ITargetRectangleObserver
    {
        void UpdateTargetRectangle(TargetRectangle rectangle);
    }
}

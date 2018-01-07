using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Interface
{
    public interface IExportPointMatrixObserable
    {
        void RegisterExportPointMatrixObserver(IExportPointMatrixObserver observer);
        void RemoveExportPointMatrixObserver(IExportPointMatrixObserver observer);
        void NotifyExportPointMatrixObservers(int row, int column);
    }
}

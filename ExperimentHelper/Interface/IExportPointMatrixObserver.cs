using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Interface
{
    public interface IExportPointMatrixObserver
    {
        void ExportPointUpdate(int row, int column);
    }
}

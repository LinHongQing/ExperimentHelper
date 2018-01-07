using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Interface
{
    public interface IExportChooseFormModel : IExportPointMatrixObserable
    {
        void Initialize();
        void Save();
        int GetRowCount();
        int GetColumnCount();
        void SetExportPoint(int row, int column);
        string GetExportPointText(int row, int column);
        bool GetExportPointAvaliable(int row, int column);
    }
}

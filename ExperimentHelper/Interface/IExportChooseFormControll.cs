using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Interface
{
    public interface IExportChooseFormControll
    {
        void Comfirm();
        void Cancel();
        void ChangeExportPointStatus(int row, int column);
    }
}

using ExperimentHelper.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Interface
{
    public interface IIterator
    {
        void First();
        void Next();
        bool IsDone();
        ExportPointMatrixItem CurrentItem();
    }
}

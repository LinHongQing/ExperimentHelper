using ExperimentHelper.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Interface
{
    public interface IMainFormModel : IResultObserable
    {
        void Initialize();
        void ForceRun(ProcessHelper.ProcessTypeFlags processType);
        void Run();
        void LoadSettings();
        void SaveSettings();
    }
}

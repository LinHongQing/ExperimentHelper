using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Interface
{
    public interface ISettingModel
    {
        void InitializeSettings();
        void LoadDefaultSettings();
        void SaveSettings();
    }
}

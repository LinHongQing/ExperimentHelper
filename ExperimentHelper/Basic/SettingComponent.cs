using ExperimentHelper.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Basic
{
    public class SettingComponent
    {
        private int shortStepDelay;         // 短步骤延迟
        private int mediumStepDelay;        // 中步骤延迟
        private int longStepDelay;          // 长步骤延迟
        private int columnDeviation;
        private int rowDeviation;
        private string searchTitle;
        private ProcessHelper.ProcessTypeFlags processType;
        private int intParam;
        private string stringParam;


        public int ColumnDeviation { get => columnDeviation; set => columnDeviation = value; }
        public int RowDeviation { get => rowDeviation; set => rowDeviation = value; }
        public int ShortStepDelay { get => shortStepDelay; set => shortStepDelay = value; }
        public int MediumStepDelay { get => mediumStepDelay; set => mediumStepDelay = value; }
        public int LongStepDelay { get => longStepDelay; set => longStepDelay = value; }
        public string SearchTitle { get => searchTitle; set => searchTitle = value; }
        public ProcessHelper.ProcessTypeFlags ProcessType { get => processType; set => processType = value; }
        public int IntParam { get => intParam; set => intParam = value; }
        public string StringParam { get => stringParam; set => stringParam = value; }

        private static SettingComponent _uniqueInstance = new SettingComponent();

        public static SettingComponent GetInstance()
        {
            return _uniqueInstance;
        }
    }
}

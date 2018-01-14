using ExperimentHelper.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Basic
{
    public class SettingComponent
    {
        private int stepDelay;              // 步骤延迟
        private int retryStepDelay;         // 重试步骤延迟
        private int maxiumNumberOfRetries;  // 最大重试次数
        private int columnDeviation;
        private int rowDeviation;
        private string searchTitle;
        private ProcessHelper.ProcessTypeFlags processType;
        private int intParam;
        private string stringParam;


        public int ColumnDeviation { get => columnDeviation; set => columnDeviation = value; }
        public int RowDeviation { get => rowDeviation; set => rowDeviation = value; }
        public int StepDelay { get => stepDelay; set => stepDelay = value; }
        public int RetryStepDelay { get => retryStepDelay; set => retryStepDelay = value; }
        public int MaximumNumberOfRetries { get => maxiumNumberOfRetries; set => maxiumNumberOfRetries = value; }
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

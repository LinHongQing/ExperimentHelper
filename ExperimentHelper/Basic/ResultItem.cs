using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Basic
{
    public class ResultItem
    {
        public enum States { OK, WARNING, ERROR, ThreadStart, ThreadFinish };

        private string logTime;
        private States logState;
        private string logMessage;

        public string LogTime { get => logTime; }
        public States LogState { get => logState; set => logState = value; }
        public string LogMessage { get => logMessage; set => logMessage = value; }

        public ResultItem(States logState, string logMessage)
        {
            Init(DateTime.Now.ToLocalTime().ToString(), logState, logMessage);
        }

        public ResultItem(string logTime, States logState, string logMessage)
        {
            Init(logTime, logState, logMessage);
        }

        private void Init(string logTime, States logState, string logMessage)
        {
            this.logTime = logTime;
            this.logState = logState;
            this.logMessage = logMessage;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[{0}] 状态: {1}, 信息: {2}", LogTime, LogState, LogMessage);
            return sb.ToString();
        }
    }
}

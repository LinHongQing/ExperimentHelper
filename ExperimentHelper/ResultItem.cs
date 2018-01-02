using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper
{
    class ResultItem
    {
        public enum States { OK, WARNING, ERROR };

        private string logTime;
        private States logState;
        private string logStateString;
        private string logMessage;
        private bool matrixSet = false;
        private bool rectangleSet = false;
        private Base.POINT[,] matrix;
        private Base.RECT rectangle;

        public string LogTime { get => logTime; set => logTime = value; }
        internal States LogState { get => logState; set => logState = value; }
        public string LogStateString { get => logStateString; set => logStateString = value; }
        public string LogMessage { get => logMessage; set => logMessage = value; }
        public Base.RECT Rectangle { get => rectangle; set => rectangle = value; }
        public Base.POINT[,] Matrix { get => matrix; set => matrix = value; }
        public bool MatrixSet { get => matrixSet; set => matrixSet = value; }
        public bool RectangleSet { get => rectangleSet; set => rectangleSet = value; }

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
            LogTime = logTime;
            LogState = logState;
            switch (logState)
            {
                case States.OK:
                    LogStateString = "正常";
                    break;
                case States.WARNING:
                    LogStateString = "警告";
                    break;
                case States.ERROR:
                    LogStateString = "错误";
                    break;
            }
            LogMessage = logMessage;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[{0}] 状态: {1}, 信息: {2}", LogTime, LogStateString, LogMessage);
            return sb.ToString();
        }
    }
}

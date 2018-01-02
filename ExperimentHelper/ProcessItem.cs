using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper
{
    class ProcessItem
    {

        private Process.ProcessTypeFlags processType;
        private Base.POINT position;
        private Base.RECT rectangle;
        private string stringParameter;
        private int integerParameter;
        private int stepDelay;

        public Process.ProcessTypeFlags ProcessType { get { return processType; } set { processType = value; } }
        public Base.POINT Position { get { return position; } set { position = value; } }
        public Base.RECT Rectangle { get { return rectangle; } set { rectangle = value; } }
        public string StringParameter { get { return stringParameter; } set { stringParameter = value; } }
        public int IntegerParameter { get { return integerParameter; } set { integerParameter = value; } }
        public int StepDelay { get { return stepDelay; } set { stepDelay = value; } }
    }
}

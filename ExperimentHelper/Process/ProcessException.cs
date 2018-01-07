using System;

namespace ExperimentHelper.Process
{
    public class ProcessException : ApplicationException
    {
        public ProcessException() : base() { }

        public ProcessException(string message) : base(message) { }

        public ProcessException(string message, Exception inner) : base(message, inner) { }
    }
}

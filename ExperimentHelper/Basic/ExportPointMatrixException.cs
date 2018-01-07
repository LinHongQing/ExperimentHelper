using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentHelper.Basic
{
    public class ExportPointMatrixException : ApplicationException
    {
        public ExportPointMatrixException() : base() { }

        public ExportPointMatrixException(string message) : base(message) { }

        public ExportPointMatrixException(string message, Exception inner) : base(message, inner) { }
    }
}

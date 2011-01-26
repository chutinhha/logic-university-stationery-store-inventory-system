using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.Exceptions
{
    [Serializable]
    public class DisbursmentException : ExceptionBase
    {
        public DisbursmentException(string message)
            : base(message)
        { }

        public DisbursmentException()
            : base()
        { }

        public DisbursmentException(string message, Exception inner)
            : base(message, inner)
        { }

        public DisbursmentException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}

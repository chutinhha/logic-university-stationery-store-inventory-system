/* Custom Project and Libraries Copyright, 2006.  Venkat, ISS */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.Exceptions
{
    [Serializable]
    public class DisbursmentItemException : ExceptionBase
    {
        public DisbursmentItemException(string message)
            : base(message)
        { }

        public DisbursmentItemException()
            : base()
        { }

        public DisbursmentItemException(string message, Exception inner)
            : base(message, inner)
        { }

        public DisbursmentItemException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}

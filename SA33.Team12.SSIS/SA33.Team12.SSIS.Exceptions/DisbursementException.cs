/* Custom Project and Libraries Copyright, 2006.  Venkat, ISS */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.Exceptions
{
    [Serializable]
    class DisbursementException:ExceptionBase
    {
        public DisbursementException(string message)
            : base(message)
        { }

        public DisbursementException()
            : base()
        { }

        public DisbursementException(string message, Exception inner)
            : base(message, inner)
        { }

        public DisbursementException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}

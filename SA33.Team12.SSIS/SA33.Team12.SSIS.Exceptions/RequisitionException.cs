using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.Exceptions
{
    [Serializable]
    public class RequisitionException : ExceptionBase
    {
        private string p;

        public RequisitionException()
            : base()
        {
        }
        public RequisitionException(string message)
            : base(message)
        {
        }

         public RequisitionException(string message, Exception inner)
            : base(message, inner)
        { }

         public RequisitionException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}

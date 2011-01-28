/***
 * Author: Sundar Aravind (A0076790U)
 * Initial Implementation: 23/Jan/2011
 * Modified on: 25/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.Exceptions
{
    [Serializable]
    public class AdjustmentVoucherException : ExceptionBase
    {
        private string p;

        public AdjustmentVoucherException()
            : base()
        {
        }
        public AdjustmentVoucherException(string message)
            : base(message)
        {
        }

         public AdjustmentVoucherException(string message, Exception inner)
            : base(message, inner)
        { }

         public AdjustmentVoucherException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}

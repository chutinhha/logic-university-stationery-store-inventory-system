/***
 * Author: Wang Pinyi (A0076771W)
 * Initial Implementation: 26/Jan/2011
 ***/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.Exceptions
{
    public class PurchaseOrderException : ExceptionBase
    {
         public PurchaseOrderException()
            : base()
        {
        }
        public PurchaseOrderException(string message)
            : base(message)
        {
        }

         public PurchaseOrderException(string message, Exception inner)
            : base(message, inner)
        { }

         public PurchaseOrderException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}

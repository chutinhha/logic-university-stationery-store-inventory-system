using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.Exceptions
{
    public class StationeryRetrievalException : ExceptionBase
    {        

        public StationeryRetrievalException()
            : base()
        {
        }
        public StationeryRetrievalException(string message)
            : base(message)
        {
        }

         public StationeryRetrievalException(string message, Exception inner)
            : base(message, inner)
        { }

         public StationeryRetrievalException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}

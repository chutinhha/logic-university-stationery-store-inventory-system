/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 24/Jan/2011
 ***/

using System;

namespace SA33.Team12.SSIS.Exceptions
{
    [Serializable]
    public class ExceptionBase : ApplicationException
    {
        public ExceptionBase() { }
        public ExceptionBase(string message) : base(message) { }
        public ExceptionBase(string message, Exception inner) : base(message, inner) { }
        protected ExceptionBase(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
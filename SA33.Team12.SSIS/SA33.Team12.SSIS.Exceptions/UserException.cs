/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;

namespace SA33.Team12.SSIS.Exceptions
{
    [Serializable]
    public class UserException:ExceptionBase
    {
        public UserException(string message)
            : base(message)
        { }

        public UserException()
            : base()
        { }

        public UserException(string message, Exception inner)
            : base(message, inner)
        { }

        public UserException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}
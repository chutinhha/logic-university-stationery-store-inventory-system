/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;

namespace SA33.Team12.SSIS.Exceptions
{
    public class UserException : ExceptionBase
    {
        public UserException(string message)
            : base(message)
        { }
    }
}
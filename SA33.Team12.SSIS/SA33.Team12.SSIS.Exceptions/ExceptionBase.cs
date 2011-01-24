/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 24/Jan/2011
 ***/

using System;

namespace SA33.Team12.SSIS.Exceptions
{
    public class ExceptionBase : ApplicationException
    {
        public ExceptionBase (string message)
            : base(message)
        { }

        public ExceptionBase()
            : base()
        { }
    }
}
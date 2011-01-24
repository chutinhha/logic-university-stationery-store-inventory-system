/***
 * Author: Naing Myo Aung (A0076803A) (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA33.Team12.SSIS.Exceptions
{
    public class UserException : ApplicationException
    {
        public UserException(string message)
            : base(message)
        { }
    }
}
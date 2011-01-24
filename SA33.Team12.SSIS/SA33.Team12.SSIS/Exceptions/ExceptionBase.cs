using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
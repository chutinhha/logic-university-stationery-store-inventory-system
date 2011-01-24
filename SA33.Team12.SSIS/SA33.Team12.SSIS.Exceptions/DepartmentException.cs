using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.Exceptions
{
    [Serializable]
    public class DepartmentException : ExceptionBase
    {
        public DepartmentException(string message)
            : base(message)
        { }

        public DepartmentException()
            : base()
        { }

        public DepartmentException(string message, Exception inner)
            : base(message, inner)
        { }

        public DepartmentException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }

}

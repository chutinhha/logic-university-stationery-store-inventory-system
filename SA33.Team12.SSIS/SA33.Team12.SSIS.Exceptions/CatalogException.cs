/***
 * Author: Victor Tong (A0066920E)
 * Initial Implementation: 01/Feb/2011
 ***/

using System;

namespace SA33.Team12.SSIS.Exceptions
{
    [Serializable]
    public class CatalogException:ExceptionBase
    {
        public CatalogException(string message)
            : base(message)
        { }

        public CatalogException()
            : base()
        { }

        public CatalogException(string message, Exception inner)
            : base(message, inner)
        { }

        public CatalogException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}
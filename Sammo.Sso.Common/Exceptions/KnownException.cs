using System;
using System.Collections.Generic;
using System.Text;

namespace Sammo.Sso.Common.Exceptions
{
    public class KnownException : Exception
    {
        public KnownException(int code)
        {
            Code = code;
        }

        public KnownException(int code, string message = null) : base(message)
        {
            Code = code;
        }

        public int Code { get; set; }
    }
}

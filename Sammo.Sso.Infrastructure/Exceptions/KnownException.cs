using Sammo.Sso.Domain.Constants;
using System;

namespace Sammo.Sso.Infrastructure.Exceptions
{
    public class KnownException : Exception
    {
        public KnownException(int code)
        {
            Code = code;
        }

        public KnownException(int code = ErrorCode.Default, string message = null) : base(message)
        {
            Code = code;
        }

        public int Code { get; set; }
    }
}

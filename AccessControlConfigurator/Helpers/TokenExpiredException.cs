using System;

namespace AccessControlConfigurator.Helpers
{
    public class TokenExpiredException : Exception
    {
        public TokenExpiredException() : base("Session expired. Please login again.")
        {
        }

        public TokenExpiredException(string message) : base(message)
        {
        }
    }
}
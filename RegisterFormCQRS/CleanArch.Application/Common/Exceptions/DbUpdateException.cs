using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Application.Common.Exceptions
{
    public class DbUpdateException : Exception
    {
        public DbUpdateException() { }

        public DbUpdateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

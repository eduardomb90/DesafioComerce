using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string? message)
        : base(message)
        {
        }
    }
}

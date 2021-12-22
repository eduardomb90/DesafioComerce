using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; private set; }
    }
}

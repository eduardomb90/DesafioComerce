using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime InsertDate { get; private set; }
        public DateTime UpdateDate { get; private set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}

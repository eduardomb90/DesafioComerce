using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public abstract class Supplier : BaseEntity
    {
        public bool Active { get; private set; }
        public DateTime InserDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        
        
        public virtual ICollection<Phone > Phones { get; private set; }
        public virtual ICollection<Product> Products { get; private set; }

        //ForeignKeys
        public Guid AddressId { get; private set; }
        public virtual Address Address { get; private set; }

        public Guid EmailId { get; private set; }
        public virtual Email Email { get; private set; }
    }
}

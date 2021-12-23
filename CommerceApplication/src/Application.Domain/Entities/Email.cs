using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class Email : BaseEntity
    {
        public string EmailAddress { get; private set; }
        

        //ForeignKey
        public Guid SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }

        protected Email()
        {
        }
    }
}

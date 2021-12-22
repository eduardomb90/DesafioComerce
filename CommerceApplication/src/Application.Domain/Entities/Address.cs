using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Reference { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public DateTime InserDate { get; private set; }
        public DateTime UpdateDate { get; private set; }

        //Foreign Key
        public Guid SupplierId { get; private set; }
        public virtual Supplier Supplier { get; private set; }
    }
}

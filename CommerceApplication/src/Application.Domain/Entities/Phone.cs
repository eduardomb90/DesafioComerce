using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class Phone : BaseEntity
    {
        public string Ddd { get; private set; }
        public string Number { get; private set; }
        
        
        
        //ForeignKey
        public Guid SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }


        protected Phone()
        {
        }

        public Phone(string ddd, string number, Supplier supplier)
        {
            Ddd = ddd;
            Number = number;
            Supplier = supplier;
        }
    }
}

using Application.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class Phone : BaseEntity
    {
        public string Ddd { get; private set; }
        public string Number { get; private set; }

        public PhoneType Type { get; private set; }



        //ForeignKey
        public Guid SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }


        protected Phone()
        {
        }

        public Phone(string ddd, string number, PhoneType type, Supplier supplier, Guid supplierId)
        {
            Ddd = ddd;
            Number = number;
            Type = type;
            Supplier = supplier;
            SupplierId = supplierId;
        }
    }
}

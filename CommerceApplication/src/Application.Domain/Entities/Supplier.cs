using Application.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public bool Active { get; private set; }


        private List<Phone> _phones = new List<Phone>();
        public IReadOnlyCollection<Phone> Phones => _phones;
        public ICollection<Product> Products { get; private set; }


        //ForeignKeys na classe menor
        public Address Address { get; private set; }
        public Email Email { get; private set; }


        protected Supplier()
        {
        }

        protected Supplier(Address address, Email email, string ddd, string phoneNumber)
        {
            Address = address;
            Email = email;

            AddPhone(ddd, phoneNumber);
        }



        //Methods
        public void AddPhone(string ddd, string phoneNumber)
        {
            if (Phones.Count == 3)
                throw new DomainException("Maximo de 3 telefones por fornecedor.");

            _phones.Add(new Phone(ddd, phoneNumber, this));
        }
    }
}

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
        

        //Foreign Key
        public Guid SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }

        protected Address()
        {
        }

        public Address(string zipCode, string street, string number, string complement, string reference, string neighborhood, string city, string state)
        {
            SetAddress(zipCode, street, number, complement, reference, neighborhood, city, state);
        }

        public void SetAddress(string zipCode, string street, string number, string complement, string reference, string neighborhood, string city, string state)
        {
            ZipCode = zipCode;
            Street = street;
            Number = number;
            Complement = complement;
            Reference = reference;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }
    }
}

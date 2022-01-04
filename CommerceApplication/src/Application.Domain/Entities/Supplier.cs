using Application.Domain.Entities.Enums;
using Application.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected Supplier(Address address, Email email, string ddd, string phoneNumber, PhoneType type, Guid supplierId)
        {
            Address = address;
            Email = email;

            AddPhone(ddd, phoneNumber, type, supplierId);
        }



        //Methods
        public void AddPhone(string ddd, string phoneNumber, PhoneType type, Guid supplierId)
        {
            if (Phones.Count == 3)
                throw new DomainException("Maximo de 3 telefones por fornecedor.");

            _phones.Add(new Phone(ddd, phoneNumber, type, this, supplierId));
        }
        
        public void RemovePhone(Phone phone)
        {
            foreach (var p in Phones.ToList())
            {
                if (p.Id == phone.Id)
                    _phones.Remove(p);
            }
        }

        public void SetAddPhone(Phone phone)
        {
            if (Phones.Count >= 3)
            {
                throw new Exception("Quantidade limite de numeros telefonicos atingido");
            }

            _phones.Add(phone);
        }

        public void SetRemovePhone(Phone phone)
        {
            if (PhoneExist(phone.Type))
            {
                _phones.Remove(phone);
            }

            throw new DomainException("Não há telefone para ser removido.");
        }

        public void SetUpdatePhone(Phone phone)
        {
            if (PhoneExist(phone.Type))
            {
                var phoneExist = Phones.Where(x => x.Type == phone.Type).FirstOrDefault();

                if (phoneExist.Number != phone.Number)
                {
                    phoneExist.SetPhone(phone);
                }
            }
            else
            {
                SetAddPhone(new Phone(phone.Ddd, phone.Number, phone.Type, phone.Supplier, phone.SupplierId));
            }
        }

        public bool PhoneExist(PhoneType phoneType)
        {
            return _phones.Where(x => x.Type == phoneType).FirstOrDefault() != null;
        }

        public void SetEmail(string email)
        {
            if (Email == null)
            {
                Email = new Email(email);
            }
            else
            {
                Email.SetEmail(email);
            }

        }

        public void SetAddress(Address address)
        {
            if (Address == null)
            {
                Address = new Address(address.ZipCode, address.Street, address.Number, address.Complement, address.Reference, address.Neighborhood, address.City, address.State);

            }
            else
            {
                Address.SetAddress(address.ZipCode, address.Street, address.Number, address.Complement, address.Reference, address.Neighborhood, address.City, address.State);
            }
        }
    }
}

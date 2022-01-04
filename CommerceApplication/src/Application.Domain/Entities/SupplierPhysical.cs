using Application.Domain.Tools;
using System;

namespace Application.Domain.Entities
{
    public class SupplierPhysical : Supplier
    {
        public string FantasyName { get; private set; }
        public string FullName { get; private set; }
        public string Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }

        protected SupplierPhysical()
        {
        }

        public SupplierPhysical(string fullName, string fantasyName, string cpf, DateTime birthDate)
        {
            FantasyName = fantasyName;
            FullName = fullName;
            Cpf = cpf;
            BirthDate = birthDate;
        }

        public bool IsValid()
        {
            return (OlderThanEighteen() && Cpf.IsCpf());
        }

        public bool OlderThanEighteen()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;

            if (BirthDate.Date > today.AddYears(-age)) age--;

            return (age >= 18);
        }

        public void SetFantasyName(string name)
        {
            FantasyName = name;
        }

        public void SetFullName(string fullName)
        {
            if (FullName != fullName)
            {
                FullName = fullName;
            }
        }

        public void SetCpf(string cpf)
        {
            if (Cpf != cpf && cpf.IsCpf())
            {
                Cpf = cpf;
            }
        }

        public void SetBirthDate(DateTime birth)
        {
            if (BirthDate != birth && OlderThanEighteen())
            {
                BirthDate = birth;
            }
        }
    }
}

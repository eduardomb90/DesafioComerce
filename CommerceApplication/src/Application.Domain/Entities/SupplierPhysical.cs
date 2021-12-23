using Application.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class SupplierPhysical : Supplier
    {
        public string FantasyName { get; private set; }
        public string FullName { get; private set; }
        public string Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }

        public bool IsValid()
        {
            return Cpf.IsCpf();
        }
    }
}

using Application.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class SupplierJuridical : Supplier
    {
        public string CompanyName { get; private set; }
        public string FantasyName { get; private set; }
        public string Cnpj { get; private set; }
        public DateTime OpenDate { get; private set; }

        public bool IsValid()
        {
            return Cnpj.IsCnpj();
        }
    }
}

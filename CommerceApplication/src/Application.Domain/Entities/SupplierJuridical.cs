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

        protected SupplierJuridical()
        {
        }

        public SupplierJuridical(string companyName, string fantasyName, string cnpj, DateTime openDate)
        {
            CompanyName = companyName;
            FantasyName = fantasyName;
            Cnpj = cnpj;
            OpenDate = openDate;
        }

        public bool IsValid()
        {
            return Cnpj.IsCnpj();
        }

        public void SetFantasyName(string name)
        {
            FantasyName = name;
        }

        public void SetCompanyName(string companyName)
        {
            CompanyName = companyName;
        }
        public void SetCnpj(string cnpj)
        {
            Cnpj = cnpj;
        }
        public void SetOpenDate(DateTime open)
        {
            OpenDate = open;
        }
    }
}

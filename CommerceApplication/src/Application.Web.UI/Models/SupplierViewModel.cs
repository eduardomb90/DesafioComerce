using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.UI.Models
{
    public class SupplierViewModel
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public AddressViewModel Address { get; set; }
        public EmailViewModel Email { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string Phone { get; set; }
        
        public string FantasyName { get; set; }


        //Physical
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }

        //Juridical
        public string CompanyName { get; set; }
        public string Cnpj { get; set; }
        public DateTime OpenDate { get; set; }
    }
}

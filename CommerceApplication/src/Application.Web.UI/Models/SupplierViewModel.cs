using Application.Web.UI.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.UI.Models
{
    public class SupplierViewModel
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        public SupplierType Type { get; set; }

        public bool Active { get; set; }
        public AddressViewModel Address { get; set; }
        public EmailViewModel Email { get; set; }
        
        
        public PhoneViewModel CellPhone { get; set; }
        public PhoneViewModel? HomePhone { get; set; }
        public PhoneViewModel? Phone { get; set; }
        
        public virtual ICollection<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

        [Required]
        public string FantasyName { get; set; }


        //Physical
        public string? FullName { get; set; }
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        //Juridical
        public string? CompanyName { get; set; }
        public string? Cnpj { get; set; }

        [DataType(DataType.Date)]
        public DateTime? OpenDate { get; set; }
    }
}

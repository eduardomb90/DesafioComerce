using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Models
{
    public class AddressViewModel
    {
        [ScaffoldColumn(false)]
        public Guid Id{ get; set; }
        
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Complement { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public string Neighborhood { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }

        public Guid SupplierId { get; set; }
    }
}

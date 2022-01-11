using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Models
{
    public class EmailViewModel
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }

        public Guid SupplierId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Models
{
    public class PhoneViewModel
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        
        
        public string Ddd { get; set; }
        
        public string Number { get; set; }

        public Guid SupplierId { get; set; }
    }
}

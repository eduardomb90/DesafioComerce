using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Models
{
    public class EmailViewModel
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }

        public Guid SupplierId { get; set; }
    }
}

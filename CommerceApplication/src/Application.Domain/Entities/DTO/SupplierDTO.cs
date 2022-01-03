using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities.DTO
{
    public class SupplierDTO
    {
        public SupplierPhysical Physical { get; set; }
        public SupplierJuridical Juridical { get; set; }
    }
}

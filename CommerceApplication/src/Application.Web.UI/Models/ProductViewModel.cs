using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string BarCode { get; set; }
        public int QuantityStock { get; set; }
        public bool Active { get; set; }
        public decimal PriceSales { get; set; }
        public decimal PricePurchase { get; set; }


        //public virtual ICollection<Image> Images { get; set; }

        public SupplierViewModel Supplier { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}

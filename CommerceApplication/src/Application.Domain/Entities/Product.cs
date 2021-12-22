using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public DateTime InsertDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public string BarCode { get; private set; }
        public int QuantityStock { get; private set; }
        public bool Active { get; private set; }
        public decimal PriceSales { get; private set; }
        public decimal PricePurchase { get; private set; }


        public virtual ICollection<Image> Images { get; set; }

        //ForeignKey
        public Guid SupplierId { get; private set; }
        public virtual Supplier Supplier { get; private set; }

        public Guid CategoryId { get; private set; }
        public virtual Category Category { get; private set; }
    }
}

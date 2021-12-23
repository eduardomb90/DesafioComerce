using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public bool Active { get; private set; }
        

        public virtual ICollection<Product> Products { get; private set; }
    }
}

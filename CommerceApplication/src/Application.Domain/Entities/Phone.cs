using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class Phone : BaseEntity
    {
        public string Ddd { get; private set; }
        public string Number { get; private set; }
        public DateTime InserDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        
        
        //ForeignKey
        public Guid SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }

    }
}

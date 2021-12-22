using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class Image : BaseEntity
    {
        public string ImagePath { get; private set; }
        public DateTime InsertDate { get; private set; }
        public DateTime UpdateDate { get; private set; }


        //ForeignKey
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
    }
}

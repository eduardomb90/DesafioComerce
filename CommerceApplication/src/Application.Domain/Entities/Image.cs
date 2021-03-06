using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities
{
    public class Image : BaseEntity
    {
        public string ImagePath { get; private set; }
       


        //ForeignKey
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }

        protected Image()
        {
        }

        public Image(string imagePath, Product product, Guid productId)
        {
            ImagePath = imagePath;
            Product = product;
            ProductId = productId;
        }

        public void SetImage(Image image)
        {
            ImagePath = image.ImagePath;
            Product = image.Product;
            ProductId = image.ProductId;
        }
    }
}

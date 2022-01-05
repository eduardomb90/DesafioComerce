using Application.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }       
        public string BarCode { get; private set; }
        public int QuantityStock { get; private set; }
        public bool Active { get; private set; }
        public decimal PriceSales { get; private set; }
        public decimal PricePurchase { get; private set; }


        private List<Image> _images = new List<Image>();
        public IReadOnlyCollection<Image> Images => _images;


        //ForeignKey
        public Guid SupplierId { get; private set; }
        public virtual Supplier Supplier { get; private set; }

        public Guid CategoryId { get; private set; }
        public virtual Category Category { get; private set; }


        public void SetName(string name)
        {
            if (!Name.Equals(name) && name != "" && name != null)
                Name = name;
        }
        public void SetBarCode(string barcode)
        {
            if (!BarCode.Equals(barcode) && barcode != "" && barcode != null)
                BarCode = barcode;
        }
        public void SetQuantityStock(int quantity)
        {
            if (QuantityStock != quantity)
                QuantityStock = quantity;
        }
        public void SetPriceSales(decimal price)
        {
            if (PriceSales != price)
                PriceSales = price;
        }
        public void SetPricePurchase(decimal price)
        {
            if (PricePurchase != price)
                PricePurchase = price;
        }
        public void SetActive(bool active)
        {
            Active = active;
        }

        public void SetSupplierId(Guid id)
        {
            if (!SupplierId.Equals(id))
                SupplierId = id;
        }
        public void SetCategoryId(Guid id)
        {
            if (!CategoryId.Equals(id))
                CategoryId = id;
        }

        public void SetAddImage(Image image)
        {
            if (Images.Count >= 5)
            {
                throw new Exception("Quantidade limite de imagens atingida");
            }

            _images.Add(image);
        }
        public void SetUpdateImage(Image image)
        {
            if (ImageExist(image.Id))
            {
                var imageExist = Images.Where(x => x.Id == image.Id).FirstOrDefault();

                imageExist.SetImage(image);
                
            }
            else
            {
                SetAddImage(new Image(image.ImagePath, image.Product, image.ProductId));
            }
        }

        public bool ImageExist(Guid id)
        {
            return _images.Where(x => x.Id == id).FirstOrDefault() != null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Models
{
    public class ImageViewModel
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }

        public Guid ProductId { get; set; }
        public ProductViewModel Product { get; set; }

        public ImageViewModel(string imagePath)
        {
            ImagePath = imagePath;
        }
    }
}

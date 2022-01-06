using Application.Domain.Entities;
using Application.Domain.Entities.Pagination;
using Application.Domain.Interfaces.Repositories;
using Application.Domain.Interfaces.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly INotifierService _notifierService;
        private readonly IProductRepository _productRepository;
        //private readonly IImageService _imageService;
        

        public ProductService(INotifierService notifierService, IProductRepository productRepository/*, IImageService imageService*/)
        {
            _notifierService = notifierService;
            _productRepository = productRepository;
            //_imageService = imageService;
        }

        public async Task<Product> GetProductById(Guid Id)
        {
            return await _productRepository.GetProductById(Id);
        }

        public async Task<PaginationViewModel<Product>> Pagination(int PageSize, int PageIndex, string query)
        {
            return await _productRepository.Pagination(PageSize, PageIndex, query);
        }

        public async Task<Product> FindById(Guid Id)
        {
            return await _productRepository.FindById(Id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task AddProduct(Product product)
        {
            await _productRepository.Insert(product);
            await _productRepository.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task Update(Product product)
        {
            var result = await _productRepository.GetProductById(product.Id);

            if (result == null)
            {
                _notifierService.AddError("Produto não localizado.");
                return;
            }

            result.SetName(product.Name);
            result.SetBarCode(product.BarCode);
            result.SetQuantityStock(product.QuantityStock);
            result.SetPriceSales(product.PriceSales);
            result.SetPricePurchase(product.PricePurchase);
            result.SetActive(product.Active);
            result.SetSupplierId(product.SupplierId);
            result.SetCategoryId(product.CategoryId);

            await EditImages(result, product);
            

            await _productRepository.Update(result);
            await _productRepository.SaveChangesAsync();
            await Task.CompletedTask;
        }

        private async Task EditImages(Product result, Product product)
        {
            if(product.Images.Count > 0)
            {
                foreach (var image in result.Images.ToList())
                {
                    if (product.Images.Where(x => x.Id == image.Id).FirstOrDefault() != null)
                        continue;
                    else
                        await _productRepository.RemoveImage(image);
                }

                foreach (var image in product.Images.ToList())
                {
                    if (result.Images.Where(x => x.Id == image.Id).FirstOrDefault() != null)
                        continue;
                    else
                        result.SetAddImage(image);
                }
            }
        }

        public async Task Remove(Guid Id)
        {
            var result = await _productRepository.GetProductById(Id);

            if (result == null) return;

            foreach (var image in result.Images)
            {
                await _productRepository.RemoveImage(image);
            }

            await _productRepository.Remove(result);
            await _productRepository.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public Guid RemoveImage(Guid id)
        {
            var productId = _productRepository.RemoveImageById(id);
            _productRepository.SaveChangesAsync();
            return productId;
        }

        private bool RunValidation<Tv, Te>(Tv validacao, Te entidade) where Tv : AbstractValidator<Te> where Te : BaseEntity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            foreach (var item in validator.Errors)
            {
                _notifierService.AddError(item.ErrorMessage);
            }
            return false;
        }
    }
}

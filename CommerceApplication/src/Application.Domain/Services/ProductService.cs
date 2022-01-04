using Application.Domain.Entities;
using Application.Domain.Entities.Pagination;
using Application.Domain.Interfaces.Repositories;
using Application.Domain.Interfaces.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly INotifierService _notifierService;
        private readonly IProductRepository _productRepository;

        public ProductService(INotifierService notifierService, IProductRepository productRepository)
        {
            _notifierService = notifierService;
            _productRepository = productRepository;
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

        public async Task AddSupplier(Product product)
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

            result.SetAddress(entity.Physical.Address);
            result.SetEmail(entity.Physical.Email.EmailAddress);

            if (entity.Juridical != null)
            {
                var resultJuridical = result as SupplierJuridical;

                resultJuridical.SetFantasyName(entity.Physical.FantasyName);

                resultJuridical.SetOpenDate(entity.Juridical.OpenDate);
                resultJuridical.SetCnpj(entity.Juridical.Cnpj);
                resultJuridical.SetCompanyName(entity.Juridical.CompanyName);

                await EditPhones(result, entity.Juridical);
            }
            else
            {
                var resultPhysical = result as SupplierPhysical;

                resultPhysical.SetFantasyName(entity.Physical.FantasyName);

                resultPhysical.SetBirthDate(entity.Physical.BirthDate);
                resultPhysical.SetCpf(entity.Physical.Cpf);
                resultPhysical.SetFullName(entity.Physical.FullName);

                await EditPhones(result, entity.Physical);
            }

            await _supplierRepository.Update(result);
            await _supplierRepository.SaveChangesAsync();
            await Task.CompletedTask;
        }

        private async Task EditPhones(Supplier result, Supplier supplier)
        {
            if (supplier.Phones.Where(x => x.Type == PhoneType.CellPhone).FirstOrDefault() != null)
            {
                result.SetUpdatePhone(supplier.Phones.Where(x => x.Type == PhoneType.CellPhone).FirstOrDefault());
                await _supplierRepository.UpdatePhone(result.Phones.Where(x => x.Type == PhoneType.CellPhone).FirstOrDefault());
            }

            if (supplier.Phones.Where(x => x.Type == PhoneType.HomePhone).FirstOrDefault() != null)
            {

                if (result.PhoneExist(PhoneType.HomePhone))
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.Type == PhoneType.HomePhone).First());
                    await _supplierRepository.UpdatePhone(result.Phones.Where(x => x.Type == PhoneType.HomePhone).FirstOrDefault());
                }
                else
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.Type == PhoneType.HomePhone).First());
                    await _supplierRepository.AddPhone(result.Phones.Where(x => x.Type == PhoneType.HomePhone).First());
                }
            }
            else
            {
                var phoneExist = result.Phones.Where(x => x.Type == PhoneType.HomePhone).FirstOrDefault();
                if (phoneExist != null)
                {
                    await _supplierRepository.RemovePhone(phoneExist);
                    //result.SetRemovePhone(phoneExist);
                }

            }

            if (supplier.Phones.Where(x => x.Type == PhoneType.Phone).FirstOrDefault() != null)
            {
                if (result.PhoneExist(PhoneType.Phone))
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.Type == PhoneType.Phone).First());
                    await _supplierRepository.UpdatePhone(result.Phones.Where(x => x.Type == PhoneType.Phone).First());

                }
                else
                {
                    result.SetUpdatePhone(supplier.Phones.Where(x => x.Type == PhoneType.Phone).First());
                    await _supplierRepository.AddPhone(result.Phones.Where(x => x.Type == PhoneType.Phone).First());
                }
            }
            else
            {
                var phoneExist = result.Phones.Where(x => x.Type == PhoneType.Phone).FirstOrDefault();
                if (phoneExist != null)
                {
                    await _supplierRepository.RemovePhone(phoneExist);
                    //result.SetRemovePhone(phoneExist);
                }
            }
        }

        public async Task Remove(Guid Id)
        {
            var result = await _productRepository.GetProductById(Id);

            if (result == null) return;

            //Primeiro remove todos as entidades que estao relacionadas ao supplier.
            if (result.Category != null)
                await _productRepository.RemoveCategory(result.Category);

            foreach (var image in result.Images)
            {
                await _productRepository.RemoveImage(image);
            }

            await _productRepository.Remove(result);
            await _productRepository.SaveChangesAsync();
            await Task.CompletedTask;
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

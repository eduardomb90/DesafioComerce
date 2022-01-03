using Application.Domain.Entities;
using Application.Domain.Entities.DTO;
using Application.Domain.Entities.Pagination;
using Application.Domain.Interfaces;
using Application.Domain.Interfaces.Repositories;
using Application.Domain.Interfaces.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly INotifierService _notifierService;
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(INotifierService notifierService, ISupplierRepository supplierRepository)
        {
            _notifierService = notifierService;
            _supplierRepository = supplierRepository;
        }

        public async Task<Supplier> GetSupplierById(Guid Id)
        {
            return await _supplierRepository.GetSupplierById(Id);
        }

        public async Task<PaginationViewModel<Supplier>> Pagination(int PageSize, int PageIndex, string query)
        {
            return await _supplierRepository.Pagination(PageSize, PageIndex, query);
        }

        public async Task<Supplier> FindById(Guid Id)
        {
            return await _supplierRepository.FindById(Id);
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _supplierRepository.GetSuppliers();
        }

        public async Task AddSupplier(SupplierDTO entity)
        {
            if (entity.Juridical != null)
            {
                await _supplierRepository.Insert(entity.Juridical);
            }
            else
            {
                await _supplierRepository.Insert(entity.Physical);
            }
            await _supplierRepository.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task Update(SupplierDTO entity)
        {
            var result = _supplierRepository.GetSupplierById(entity.Physical == null ? entity.Juridical.Id : entity.Physical.Id);

            if (entity.Juridical != null)
            {
                await _supplierRepository.UpdateAddress(entity.Juridical.Address);
                await _supplierRepository.UpdateEmail(entity.Juridical.Email);


                foreach (var phone in entity.Juridical.Phones)
                {
                    await _supplierRepository.UpdatePhone(phone);
                }

                await _supplierRepository.Update(entity.Juridical);
            }
            else
            {
                //await _supplierRepository.UpdateAddress(entity.Physical.Address);
                //await _supplierRepository.UpdateEmail(entity.Physical.Email);

                //foreach (var phone in entity.Physical.Phones)
                //{
                //    await _supplierRepository.UpdatePhone(phone);
                //}

                //await _supplierRepository.Update(entity.Physical);
                await _supplierRepository.UpdateSupplier(entity.Physical);
            }

            await _supplierRepository.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task Remove(Guid Id)
        {
            var result = await _supplierRepository.GetSupplierById(Id);

            if (result == null) return;

            //Primeiro remove todos as entidades que estao relacionadas ao supplier.
            if (result.Address != null)
                await _supplierRepository.RemoveAddress(result.Address);

            if (result.Email != null)
                await _supplierRepository.RemoveEmail(result.Email);

            foreach (var phone in result.Phones)
            {
                await _supplierRepository.RemovePhone(phone);
            }

            await _supplierRepository.Remove(result);
            await _supplierRepository.SaveChangesAsync();
            await Task.CompletedTask;
        }

        private bool RunValidation<TV, TE>(TV validator, TE validate) where TV : AbstractValidator<TV> where TE : BaseEntity
        {
            return false;
        }
    }
}

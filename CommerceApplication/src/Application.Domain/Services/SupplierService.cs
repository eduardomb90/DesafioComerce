using Application.Domain.Entities;
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

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _supplierRepository.GetSuppliers();
        }

        public async Task AddSupplier(Supplier supplier)
        {
            await _supplierRepository.Insert(supplier);
            await Task.CompletedTask;
        }

        private bool RunValidation<TV, TE>(TV validator, TE validate) where TV : AbstractValidator<TV> where TE : BaseEntity
        {
            return false;
        }
    }
}

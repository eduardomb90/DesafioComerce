using Application.Domain.Entities;
using Application.Domain.Interfaces;
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
        private readonly IBaseRepository _baseRepository;

        public SupplierService(INotifierService notifierService, IBaseRepository baseRepository)
        {
            _notifierService = notifierService;
            _baseRepository = baseRepository;
        }

        public async Task AddSupplier()
        {

        }

        private bool RunValidation<TV, TE>(TV validator, TE validate) where TV : AbstractValidator<TV> where TE : BaseEntity
        {
            return false;
        }
    }
}

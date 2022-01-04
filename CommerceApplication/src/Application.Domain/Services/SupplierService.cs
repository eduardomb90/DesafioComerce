using Application.Domain.Entities;
using Application.Domain.Entities.DTO;
using Application.Domain.Entities.Enums;
using Application.Domain.Entities.Pagination;
using Application.Domain.Interfaces;
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

        //public async Task Update(SupplierDTO entity)
        //{
        //    var result = _supplierRepository.GetSupplierById(entity.Physical == null ? entity.Juridical.Id : entity.Physical.Id);

        //    if (entity.Juridical != null)
        //    {
        //        await _supplierRepository.UpdateAddress(entity.Juridical.Address);
        //        await _supplierRepository.UpdateEmail(entity.Juridical.Email);


        //        foreach (var phone in entity.Juridical.Phones)
        //        {
        //            await _supplierRepository.UpdatePhone(phone);
        //        }

        //        await _supplierRepository.Update(entity.Juridical);
        //    }
        //    else
        //    {
        //        //await _supplierRepository.UpdateAddress(entity.Physical.Address);
        //        //await _supplierRepository.UpdateEmail(entity.Physical.Email);

        //        //foreach (var phone in entity.Physical.Phones)
        //        //{
        //        //    await _supplierRepository.UpdatePhone(phone);
        //        //}

        //        //await _supplierRepository.Update(entity.Physical);
        //        await _supplierRepository.UpdateSupplier(entity.Physical);
        //    }

        //    await _supplierRepository.SaveChangesAsync();
        //    await Task.CompletedTask;
        //}

        public async Task Update(SupplierDTO entity)
        {
            var result = await _supplierRepository.GetSupplierById(entity.Physical == null ? entity.Juridical.Id : entity.Physical.Id);

            if (result == null)
            {
                _notifierService.AddError("Fornecedor não localizado.");
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

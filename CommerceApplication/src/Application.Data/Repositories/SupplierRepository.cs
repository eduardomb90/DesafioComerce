using Application.Data.Context;
using Application.Domain.Entities;
using Application.Domain.Entities.DTO;
using Application.Domain.Entities.Pagination;
using Application.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Application.Data.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository, IDisposable
    {
        public SupplierRepository(CommerceDbContext commerceDbContext) : base(commerceDbContext)
        {
        }

        public async Task<Supplier> GetSupplierById(Guid Id)
        {
            return await _commerceDbContext.Suppliers
                .Include(x => x.Phones)
                .Include(x => x.Email)
                .Include(x => x.Address)
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _commerceDbContext.Suppliers
                .Include(x => x.Phones)
                .Include(x => x.Email)
                .Include(x => x.Address)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PaginationViewModel<Supplier>> Pagination(int PageSize, int PageIndex, string query)
        {
            IPagedList<Supplier> list;

            if (!string.IsNullOrEmpty(query))
            {
                list = await _commerceDbContext.Suppliers.Where(x => x.Email.EmailAddress.Contains(query) /*|| x.FullName.Contains(query)*/).AsNoTracking().ToPagedListAsync(PageIndex, PageSize);
            }
            else
            {
                list = await _commerceDbContext.Suppliers.Include(x => x.Phones)
                                .Include(x => x.Email)
                                .Include(x => x.Address)
                                .AsNoTracking()
                                .ToPagedListAsync(PageIndex, PageSize);
            }

            return new PaginationViewModel<Supplier>()
            {
                List = list.ToList(),
                PageIndex = PageIndex,
                PageSize = PageSize,
                Query = query,
                TotalResult = list.TotalItemCount
            };
        }

        public async Task RemoveAddress(Address address)
        {
            _commerceDbContext.Addresses.Remove(address);
            await Task.CompletedTask;
        }

        public async Task RemoveEmail(Email email)
        {
            _commerceDbContext.Emails.Remove(email);
            await Task.CompletedTask;
        }

        public async Task RemovePhone(Phone phone)
        {
            _commerceDbContext.Phones.Remove(phone);
            await Task.CompletedTask;
        }

        public async Task UpdateAddress(Address address)
        {
            _commerceDbContext.Entry(address).State = EntityState.Modified;
            _commerceDbContext.Addresses.Update(address);
            //await _commerceDbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task UpdateEmail(Email email)
        {
            _commerceDbContext.Entry(email).State = EntityState.Modified;
            _commerceDbContext.Emails.Update(email);
            //await _commerceDbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task UpdatePhone(Phone phone)
        {
            _commerceDbContext.Entry(phone).State = EntityState.Modified;
            _commerceDbContext.Phones.Update(phone);
            //await _commerceDbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task UpdateSupplier(Supplier entity)
        {
            var existingSupplier = await GetSupplierById(entity.Id);

            if (existingSupplier != null)
            {
                // Update parent
                _commerceDbContext.Entry(existingSupplier).CurrentValues.SetValues(entity);

                // Delete Phones
                foreach (var existingPhone in existingSupplier.Phones.ToList())
                {
                    if (!entity.Phones.Any(phone => phone.Id == existingPhone.Id))
                    {
                        _commerceDbContext.Phones.Remove(existingPhone);
                        existingSupplier.RemovePhone(existingPhone);
                    }
                }

                // Update and Insert children
                foreach (var phoneModel in entity.Phones)
                {
                    var existingPhone = existingSupplier.Phones
                        .Where(phone => phone.Id == phoneModel.Id && phone.Id != default(Guid))
                        .SingleOrDefault();

                    if (existingPhone != null)
                        // Update child
                        _commerceDbContext.Entry(existingPhone).CurrentValues.SetValues(phoneModel);
                    else
                    {
                        // Insert child                        
                        existingSupplier.AddPhone(phoneModel.Ddd, phoneModel.Number,
                                                    phoneModel.Type,
                                                        phoneModel.SupplierId);
                    }

                    await _commerceDbContext.SaveChangesAsync();
                }
            }
        }


    }
}

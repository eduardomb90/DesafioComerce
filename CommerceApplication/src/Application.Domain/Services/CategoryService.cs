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
    public class CategoryService : ICategoryService
    {
        private readonly INotifierService _notifierService;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(INotifierService notifierService, ICategoryRepository categoryRepository)
        {
            _notifierService = notifierService;
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetCategoryById(Guid Id)
        {
            return await _categoryRepository.GetCategoryById(Id);
        }

        public async Task<PaginationViewModel<Category>> Pagination(int PageSize, int PageIndex, string query)
        {
            return await _categoryRepository.Pagination(PageSize, PageIndex, query);
        }

        public async Task<Category> FindById(Guid Id)
        {
            return await _categoryRepository.FindById(Id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _categoryRepository.GetCategories();
        }

        public async Task AddCategory(Category category)
        {
            await _categoryRepository.Insert(category);
            await _categoryRepository.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task Update(Category category)
        {
            await _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task Remove(Guid Id)
        {
            var result = await _categoryRepository.FindById(Id);

            if (result == null) return;

            await _categoryRepository.Remove(result);
            await _categoryRepository.SaveChangesAsync();
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


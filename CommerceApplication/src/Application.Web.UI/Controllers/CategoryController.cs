using Application.Domain.Entities;
using Application.Domain.Entities.Pagination;
using Application.Domain.Interfaces.Services;
using Application.Web.UI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly INotifierService _notifierService;

        public CategoryController(ICategoryService categoryService, IMapper mapper, INotifierService notifierService)
        : base(mapper, notifierService)
        {
            _categoryService = categoryService;
            _notifierService = notifierService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int PageSize = 1, int PageIndex = 1, string query = null)
        {
            var result = await _categoryService.Pagination(PageSize, PageIndex, query);

            return View(new PaginationViewModel<CategoryViewModel>()
            {
                List = _mapper.Map<IEnumerable<CategoryViewModel>>(result.List),
                PageIndex = result.PageIndex,
                PageSize = result.PageSize,
                Query = result.Query,
                TotalResult = result.TotalResult,
                ReferenceAction = "Index"
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var category = _mapper.Map<Category>(model);

            await _categoryService.AddCategory(category);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null) return BadRequest();

            var model = _mapper.Map<CategoryViewModel>(category);

            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var category = _mapper.Map<Category>(model);

            await _categoryService.Update(category);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryService.FindById(id);

            if (category == null) return NotFound();

            var model = _mapper.Map<CategoryViewModel>(category);

            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            await _categoryService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

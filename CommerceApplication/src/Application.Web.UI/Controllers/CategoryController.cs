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

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        : base(mapper)
        {
            _categoryService = categoryService;
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

            //var suppliers = await _supplierService.GetSuppliers();

            //var model = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);

            //return View(model);
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
            //AddImages(model, supplier.Physical);

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

            //var result = await _supplierValidation.ValidateAsync(model);

            //if (!result.IsValid)
            //{
            //    foreach (var failure in result.Errors)
            //    {
            //        Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
            //    }

            //    return View(model);
            //}

            var category = _mapper.Map<Category>(model);

            await _categoryService.Update(category);

            //if (!ValidOperation()) return View(model);

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

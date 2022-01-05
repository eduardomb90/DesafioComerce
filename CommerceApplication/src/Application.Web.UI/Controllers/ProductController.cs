﻿using Application.Domain.Entities;
using Application.Domain.Entities.Pagination;
using Application.Domain.Interfaces.Services;
using Application.Web.UI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductController(IProductService productService,
                                ICategoryService categoryService,
                                ISupplierService supplierService,
                                IHostingEnvironment hostingEnvironment,
                                IMapper mapper)
        : base(mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int PageSize = 1, int PageIndex = 1, string query = null)
        {
            var result = await _productService.Pagination(PageSize, PageIndex, query);

            return View(new PaginationViewModel<ProductViewModel>()
            {
                List = _mapper.Map<IEnumerable<ProductViewModel>>(result.List),
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
            var viewModel = new ProductViewModel();
            ViewBag.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.GetCategories());
            ViewBag.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierService.GetSuppliers());
            
            return View(viewModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            await AddImage(model);

            var product = _mapper.Map<Product>(model);

            await _productService.AddProduct(product);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null) return BadRequest();

            var model = _mapper.Map<ProductViewModel>(product);

            //foreach (var phone in product.Images)
            //{
            //    if (phone.Type == PhoneType.CellPhone) model.CellPhone = _mapper.Map<PhoneViewModel>(phone);
            //    if (phone.Type == PhoneType.HomePhone) model.HomePhone = _mapper.Map<PhoneViewModel>(phone);
            //    if (phone.Type == PhoneType.Phone) model.Phone = _mapper.Map<PhoneViewModel>(phone);
            //}

            ViewBag.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.GetCategories());
            ViewBag.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierService.GetSuppliers());

            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
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

            var product = _mapper.Map<Product>(model);

            //AddImages(model, supplier.Juridical);
            
            await _productService.Update(product);

            //if (!ValidOperation()) return View(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null) return NotFound();

            var model = _mapper.Map<ProductViewModel>(product);

            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            await _productService.Remove(id);
            return RedirectToAction(nameof(Index));
        }


        private async Task AddImage(ProductViewModel model)
        {
            string uniqueFileName = null;
            if (model.ImagesUpload != null && model.ImagesUpload.Count > 0)
            {
                for (int i = 1; i < 5; i++)
                {
                    var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagesUpload[i].FileName;
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImagesUpload[i].CopyToAsync(fileStream);
                    }

                    model.Images.Add(new ImageViewModel(uniqueFileName));
                }

                //foreach (IFormFile image in model.ImagesUpload)
                //{
                //    var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                //    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                //    using (var fileStream = new FileStream(filePath, FileMode.Create))
                //    {
                //        await image.CopyToAsync(fileStream);
                //    }

                //    model.Images.Add(new ImageViewModel(uniqueFileName));
                //}
            }
        }
    }
}
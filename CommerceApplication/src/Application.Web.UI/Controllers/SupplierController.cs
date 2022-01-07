using Application.Domain.Entities;
using Application.Domain.Entities.DTO;
using Application.Domain.Entities.Enums;
using Application.Domain.Entities.Pagination;
using Application.Domain.Interfaces.Services;
using Application.Web.UI.Models;
using Application.Web.UI.Models.Enums;
using Application.Web.UI.Tools;
using Application.Web.UI.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Web.UI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SupplierController : BaseController
    {
        protected readonly ISupplierService _supplierService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductService _productService;
        private readonly INotifierService _notifierService;
        private readonly SupplierViewModelValidation _supplierValidation;

        public SupplierController(ISupplierService supplierService, IHostingEnvironment hostingEnvironment,
                                  IProductService productService, IMapper mapper, INotifierService notifierService,
                                  SupplierViewModelValidation supplierValidation) 
        : base(mapper, notifierService)
        {
            _supplierService = supplierService;
            _hostingEnvironment = hostingEnvironment;
            _productService = productService;
            _notifierService = notifierService;
            _supplierValidation = supplierValidation;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int PageSize = 5, int PageIndex = 1, string query = null)
        {
            var result = await _supplierService.Pagination(PageSize, PageIndex, query);

            return View(new PaginationViewModel<SupplierViewModel>()
            {
                List = _mapper.Map<IEnumerable<SupplierViewModel>>(result.List),
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
        public async Task<IActionResult> Create(SupplierViewModel model)
        {
            //if (!ModelState.IsValid) return View(model);

            var result = await _supplierValidation.ValidateAsync(model);

            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }

                return View(model);
            }

            SupplierDTO supplier = new SupplierDTO();

            switch (model.Type)
            {
                case Models.Enums.SupplierType.Physical:
                    supplier.Physical = _mapper.Map<SupplierPhysical>(model);
                    AddPhones(model, supplier.Physical);
                    break;
                case Models.Enums.SupplierType.Juridical:
                    supplier.Juridical = _mapper.Map<SupplierJuridical>(model);
                    AddPhones(model, supplier.Juridical);
                    break;
                default:
                    return BadRequest();
            }

            await _supplierService.AddSupplier(supplier);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplier = await _supplierService.GetSupplierById(id);

            if (supplier == null) return BadRequest();

            var model = _mapper.Map<SupplierViewModel>(supplier);

            foreach (var phone in supplier.Phones)
            {
                if (phone.Type == PhoneType.CellPhone) model.CellPhone = _mapper.Map<PhoneViewModel>(phone);
                if (phone.Type == PhoneType.HomePhone) model.HomePhone = _mapper.Map<PhoneViewModel>(phone);
                if (phone.Type == PhoneType.Phone) model.Phone = _mapper.Map<PhoneViewModel>(phone);
            }

            if (model.Cpf != null) model.Type = SupplierType.Physical;
            else model.Type = SupplierType.Juridical;

            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(SupplierViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _supplierValidation.ValidateAsync(model);

            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }

                return View(model);
            }

            SupplierDTO supplier = new SupplierDTO();

            switch (model.Type)
            {
                case SupplierType.Physical:
                    supplier.Physical = _mapper.Map<SupplierPhysical>(model);
                    AddPhones(model, supplier.Physical);
                    break;
                case SupplierType.Juridical:
                    supplier.Juridical = _mapper.Map<SupplierJuridical>(model);
                    AddPhones(model, supplier.Juridical);
                    break;
                default:
                    return BadRequest();
            }

            await _supplierService.Update(supplier);

            //if (!ValidOperation()) return View(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplier = await _supplierService.FindById(id);

            if (supplier == null) return NotFound();

            var model = _mapper.Map<SupplierViewModel>(supplier);

            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            await _supplierService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ExportSupplierXlsx()
        {
            var suppliersList = await _supplierService.GetSuppliers();
            var productsList = await _productService.GetProducts();

            foreach (var supplier in suppliersList)
            {
                foreach (var product in productsList)
                {
                    if (supplier.Id == product.SupplierId)
                        supplier.Products.Add(product);
                }
            }

            var listModel = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliersList);

            var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "reports");
            var uniqueFileName = Guid.NewGuid().ToString() + "_Relatorio.xlsx";
            var filePath = Path.Combine(uploadFolder, uniqueFileName);

            GenerateExcel.Create(filePath, listModel);

            return RedirectToAction(nameof(Index));
        }

        private static void AddPhones(SupplierViewModel model, Supplier supplier)
        {
            if (model.CellPhone.Ddd != null)
                supplier.AddPhone(model.CellPhone.Ddd, model.CellPhone.Number, PhoneType.CellPhone, model.CellPhone.SupplierId);

            if (model.HomePhone.Ddd != null)
                supplier.AddPhone(model.HomePhone.Ddd, model.HomePhone.Number, PhoneType.HomePhone, model.HomePhone.SupplierId);

            if (model.Phone.Ddd != null)
                supplier.AddPhone(model.Phone.Ddd, model.Phone.Number, PhoneType.Phone, model.Phone.SupplierId);
        }
    }
}

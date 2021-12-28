using Application.Domain.Entities;
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
    public class SupplierController : BaseController
    {
        public SupplierController(ISupplierService supplierService, IMapper mapper) 
        : base(supplierService, mapper)
        {
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierService.GetSuppliers();

            var model = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel model)
        {
            var supplier = _mapper.Map<SupplierPhysical>(model);

            if (!string.IsNullOrEmpty(model.CellPhone))
                supplier.AddPhone(model.CellPhone[..2], model.CellPhone[2..]);

            if (!string.IsNullOrEmpty(model.HomePhone))
                supplier.AddPhone(model.HomePhone[..2], model.HomePhone[2..]);

            if (!string.IsNullOrEmpty(model.Phone))
                supplier.AddPhone(model.Phone[..2], model.Phone[2..]);

            await _supplierService.AddSupplier(supplier);

            return View();
        }


    }
}

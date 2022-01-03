﻿using Application.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly ISupplierService _supplierService;
        protected readonly IMapper _mapper;

        public BaseController(ISupplierService supplierService, IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }
    }
}
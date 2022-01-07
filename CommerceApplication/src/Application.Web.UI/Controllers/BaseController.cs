using Application.Domain.Interfaces.Services;
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
        protected readonly IMapper _mapper;
        private readonly INotifierService _notifierService;

        public BaseController(IMapper mapper,
                              INotifierService notifierService)
        {
            _mapper = mapper;
            _notifierService = notifierService;
        }

        protected bool OperationValid()
        {
            return _notifierService.HasError();
        }
    }
}

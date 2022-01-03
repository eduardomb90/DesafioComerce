using Application.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotifierService _notifierService;

        public SummaryViewComponent(INotifierService notifierService)
        {
            _notifierService = notifierService;
        }

        public IViewComponentResult Invoke()
        {
            var erros = _notifierService.GetErrors().Select(c => c.Error).ToList();

            erros.ForEach(x => ViewData.ModelState.AddModelError(string.Empty, x + " <br />"));

            return View(erros);
        }
    }
}

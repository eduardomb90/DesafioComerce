using Application.Domain.Entities.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.UI.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedViewModel modelPagination)
        {
            return View(modelPagination);
        }
    }
}

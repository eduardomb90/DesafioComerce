using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities.Pagination
{
    public class PaginationViewModel<T> : IPagedViewModel where T : class
    {
        public string ReferenceAction { get; set; }
        public IEnumerable<T> List { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
        public int TotalResult { get; set; }
        public double TotalPages => Math.Ceiling((double)TotalResult / PageSize);
    }

    public interface IPagedViewModel
    {
        public string ReferenceAction { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
        public int TotalResult { get; set; }
        public double TotalPages { get; }
    }
}

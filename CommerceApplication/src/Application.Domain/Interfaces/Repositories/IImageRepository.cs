using Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Interfaces.Repositories
{
    public interface IImageRepository : IBaseRepository<Image>
    {
        Task<Image> GetImageById(Guid Id);
    }
}

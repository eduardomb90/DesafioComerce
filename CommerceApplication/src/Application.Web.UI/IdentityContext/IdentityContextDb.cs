using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.AppDbContext
{
    public class IdentityContextDb : IdentityDbContext
    {
        public IdentityContextDb(DbContextOptions<IdentityContextDb> options) : base(options)
        {
        }
    }
}

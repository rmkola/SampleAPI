using SampleAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.Application.Repositories
{
    public interface IProductReadRepository : IReadRepository<Product>
    {
    }
}

using SampleAPI.Application.Repositories;
using SampleAPI.Domain.Entities;
using SampleAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.Persistence.Repositories
{
    public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
    {
        public BasketReadRepository(SampleAPIDbContext context) : base(context)
        {
        }
    }
}

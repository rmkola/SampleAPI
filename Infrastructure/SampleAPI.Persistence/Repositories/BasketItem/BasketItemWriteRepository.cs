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
    public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository
    {
        public BasketItemWriteRepository(SampleAPIDbContext context) : base(context)
        {
        }
    }
}

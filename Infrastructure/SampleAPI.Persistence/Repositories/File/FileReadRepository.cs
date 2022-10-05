using SampleAPI.Application.Repositories;
using SampleAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<SampleAPI.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(SampleAPIDbContext context) : base(context)
        {
        }
    }
}

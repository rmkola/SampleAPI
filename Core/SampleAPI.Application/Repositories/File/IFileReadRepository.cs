using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.Application.Repositories
{
    public interface IFileReadRepository : IReadRepository<SampleAPI.Domain.Entities.File>
    {
    }
}

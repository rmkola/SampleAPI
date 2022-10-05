using F = SampleAPI.Domain.Entities;

namespace SampleAPI.Application.Repositories
{
    public interface IFileWriteRepository : IWriteRepository<F::File>
    {
    }
}

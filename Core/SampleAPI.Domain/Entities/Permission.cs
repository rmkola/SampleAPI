using SampleAPI.Domain.Entities.Common;

namespace SampleAPI.Domain.Entities
{
    public class Permission: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}

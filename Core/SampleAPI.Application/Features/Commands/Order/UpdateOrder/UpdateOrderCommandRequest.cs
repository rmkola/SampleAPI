using MediatR;

namespace SampleAPI.Application.Features.Commands.Order.UpdateOrder
{
    public class UpdateOrderCommandRequest : IRequest<UpdateOrderCommandResponse>
    {
        public string Description { get; set; }
        public string Address { get; set; }
    }
}

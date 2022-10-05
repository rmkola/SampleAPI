﻿using MediatR;

namespace SampleAPI.Application.Features.Commands.Basket.UpdateQuantity
{
    public class UpdateQuantityCommandRequest : IRequest<UpdateQuantityCommandResponse>
    {
        public string? BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
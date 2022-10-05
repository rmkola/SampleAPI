namespace SampleAPI.Application.DTOs.Order
{
    public class UpdateOrder
    {
        public string BasketId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}

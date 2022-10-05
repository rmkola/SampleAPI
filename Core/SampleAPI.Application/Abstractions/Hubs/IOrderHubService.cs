namespace SampleAPI.Application.Abstractions.Hubs
{
    public interface IOrderHubService
    {
        Task OrderAddedMessageAsync(string message);
        Task OrderDeletedMessageAsync(string message);
        Task OrderUpdatedMessageAsync(string message);
    }
}

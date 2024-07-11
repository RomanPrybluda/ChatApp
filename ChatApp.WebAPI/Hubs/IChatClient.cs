namespace ChatApp.WebAPI.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);

        Task ChatDeleted(string message);
    }
}
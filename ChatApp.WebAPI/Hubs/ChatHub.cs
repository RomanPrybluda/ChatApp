using Microsoft.AspNetCore.SignalR;

namespace ChatApp.WebAPI
{
    public class ChatHub : Hub
    {

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            await Clients.All.SendAsync("ReceiveMessage", "System", "A user has left the chat.");
            await base.OnDisconnectedAsync(exception);
        }
    }
}

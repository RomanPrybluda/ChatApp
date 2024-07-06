using ChatApp.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.WebAPI
{

    public class ChatHub : Hub<IChatClient>
    {

        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId}: {message}");
        }


        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        //public async Task SendNotification(string content)
        //{
        //    await Clients.All.SendAsync("ReceiveNotification", content);
        //}



        //public async Task SendNotification(string content)
        //{
        //    await Clients.All.ReceiveNotification(content);
        //}





        //public override async Task OnConnectedAsync()
        //{
        //    await Clients.Caller.SendAsync("ReceiveMessage", "System", "Welcome to the chat!");
        //    await base.OnConnectedAsync();
        //}

        //public override async Task OnDisconnectedAsync(System.Exception exception)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", "System", "A user has left the chat.");
        //    await base.OnDisconnectedAsync(exception);
        //}




        //public async Task JoinChat()
        //{
        //    await Clients.Caller.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
        //    await base.OnConnectedAsync();
        //}




        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");
        //}

        //public override async Task OnDisconnectedAsync(System.Exception exception)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", "System", "A user has left the chat.");
        //    await base.OnDisconnectedAsync(exception);
        //}
    }
}

using ChatApp.DOMAIN;
using ChatApp.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ChatApp.WebAPI
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly ChatService _chatService;
        private readonly MessageService _messageService;

        private static readonly ConcurrentDictionary<string, string> UserConnections = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentDictionary<string, string> ChatCreators = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> ChatRooms = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();

        public ChatHub(ChatService chatService, MessageService messageService)
        {
            _chatService = chatService;
            _messageService = messageService;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.ReceiveMessage("Welcome to the chat!");
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string userId, string chatId, string message)
        {
            if (ChatRooms.ContainsKey(chatId) && ChatRooms[chatId].ContainsKey(userId))
            {
                var request = new CreateMessageDTO
                {
                    Text = message,
                    ChatId = int.Parse(chatId),
                    UserId = int.Parse(userId)
                };

                await _messageService.CreateMessageAsync(request);
                await Clients.All.ReceiveMessage($"{userId}: {message}");
            }
            else
            {
                await Clients.Caller.ReceiveMessage("You are not part of this chat.");
            }
        }

        public async Task JoinChat(string userId, string chatId)
        {
            if (!ChatRooms.ContainsKey(chatId))
            {
                ChatRooms[chatId] = new ConcurrentDictionary<string, string>();
            }

            ChatRooms[chatId][userId] = Context.ConnectionId;
            UserConnections[Context.ConnectionId] = chatId;
            await Clients.All.ReceiveMessage($"{userId} has joined the chat {chatId}");
        }

        public async Task CreateChat(string chatName, string userId)
        {
            if (!ChatRooms.ContainsKey(chatName))
            {
                var request = new CreateChatDTO
                {
                    ChatName = chatName
                };

                var chat = await _chatService.CreateChatAsync(request, int.Parse(userId));
                ChatRooms[chatName] = new ConcurrentDictionary<string, string>();
                ChatCreators[chatName] = userId;
                await Clients.Caller.ReceiveMessage($"Chat {chat.ChatName} created successfully.");
            }
            else
            {
                await Clients.Caller.ReceiveMessage($"Chat with name {chatName} already exists.");
            }
        }

        public async Task DeleteChat(string userId, string chatId)
        {
            if (ChatCreators.TryGetValue(chatId, out var creatorId) && creatorId == userId)
            {
                if (ChatRooms.TryRemove(chatId, out var connections))
                {
                    ChatCreators.TryRemove(chatId, out _);
                    await _chatService.DeleteChatAsync(int.Parse(chatId), int.Parse(userId));
                    foreach (var connectionId in connections.Values)
                    {
                        await Clients.Client(connectionId).ChatDeleted("The chat has been deleted by the creator.");
                        await Clients.Client(connectionId).ReceiveMessage("You have been disconnected.");
                    }
                }
            }
            else
            {
                await Clients.Caller.ReceiveMessage("There are no permissions to do the operation.");
            }
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            if (UserConnections.TryRemove(Context.ConnectionId, out var chatId))
            {
                if (ChatRooms.TryGetValue(chatId, out var connections))
                {
                    var userId = connections.FirstOrDefault(c => c.Value == Context.ConnectionId).Key;
                    if (userId != null)
                    {
                        connections.TryRemove(userId, out _);
                        await Clients.All.ReceiveMessage($"{userId} has left the chat {chatId}");
                    }
                }
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}

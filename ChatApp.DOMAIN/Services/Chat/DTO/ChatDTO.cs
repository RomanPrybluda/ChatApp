using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class ChatDTO
    {
        public int ChatId { get; set; }

        public string ChatName { get; set; } = string.Empty;

        public static ChatDTO ChatToChatDTO(Chat chat)
        {
            return new ChatDTO
            {
                ChatId = chat.ChatId,
                ChatName = chat.ChatName
            };
        }

    }
}

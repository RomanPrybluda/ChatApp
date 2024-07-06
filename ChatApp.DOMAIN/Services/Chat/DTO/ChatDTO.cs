using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class ChatDTO
    {

        public int ChatId { get; set; }

        public string ChatName { get; set; } = string.Empty;

        public int CreatorId { get; set; }

        public static ChatDTO ChatToChatDTO(Chat chat)
        {
            return new ChatDTO
            {
                ChatId = chat.ChatId,
                ChatName = chat.ChatName,
                CreatorId = chat.CreatorUserId
            };
        }

    }
}

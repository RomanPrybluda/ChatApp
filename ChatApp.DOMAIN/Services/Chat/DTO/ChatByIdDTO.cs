using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class ChatByIdDTO
    {

        public int ChatId { get; set; }

        public string ChatName { get; set; } = string.Empty;

        public int CreatorId { get; set; }

        public List<ChatMessageDTO> Messages { get; set; } = new List<ChatMessageDTO>();

        public static ChatByIdDTO ChatToChatByIdDTO(Chat chat)
        {
            return new ChatByIdDTO
            {
                ChatId = chat.ChatId,
                ChatName = chat.ChatName,
                CreatorId = chat.CreatorUserId,
                Messages = chat.Messages.Select(ChatMessageDTO.MessageToMessageDTO).ToList()
            };
        }

    }
}

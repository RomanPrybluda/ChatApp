using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class ChatByIdDTO
    {

        public int ChatId { get; set; }

        public string ChatName { get; set; } = string.Empty;

        public int CreatorId { get; set; }

        public List<MessageDTO> Messages { get; set; } = new List<MessageDTO>();

        public static ChatByIdDTO ChatToChatByIdDTO(Chat chat)
        {
            return new ChatByIdDTO
            {
                ChatId = chat.ChatId,
                ChatName = chat.ChatName,
                CreatorId = chat.CreatorUserId,
                Messages = chat.Messages.Select(MessageDTO.MessageToMessageDTO).ToList()
            };
        }

    }
}

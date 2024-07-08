using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class CreateMessageDTO
    {
        public string Text { get; set; } = string.Empty;

        public int ChatId { get; set; }

        public int UserId { get; set; }

        public static Message CreateMessageDTOToMessage(CreateMessageDTO dto)
        {
            return new Message
            {
                Text = dto.Text,
                ChatId = dto.ChatId,
                UserId = dto.UserId
            };
        }
    }
}
using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class UpdateMessageDTO
    {
        public string Text { get; set; } = string.Empty;

        public int ChatId { get; set; }

        public int UserId { get; set; }

        public static void UpdateMessage(Message message, UpdateMessageDTO dto)
        {
            message.Text = dto.Text;
            message.ChatId = dto.ChatId;
            message.UserId = dto.UserId;
        }
    }
}
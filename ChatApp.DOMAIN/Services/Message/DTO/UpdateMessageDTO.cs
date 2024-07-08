using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class UpdateMessageDTO
    {
        public string Text { get; set; } = string.Empty;

        public static void UpdateMessage(Message message, UpdateMessageDTO dto)
        {
            message.Text = dto.Text;
        }
    }
}
using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class ChatMessageDTO
    {

        public int MessageId { get; set; }

        public string Text { get; set; }

        public DateTime SentAt { get; set; }

        public string UserName { get; set; }

        public static ChatMessageDTO MessageToMessageDTO(Message message)
        {
            return new ChatMessageDTO
            {
                MessageId = message.MessageId,
                Text = message.Text,
                SentAt = message.SentAt,
                UserName = message.User.UserName
            };
        }
    }
}

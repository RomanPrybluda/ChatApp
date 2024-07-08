using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class MessageDTO
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }

        public static MessageDTO MessageToMessageDTO(Message message)
        {
            return new MessageDTO
            {
                MessageId = message.MessageId,
                Text = message.Text,
                SentAt = message.SentAt,
                ChatId = message.ChatId,
                UserId = message.UserId
            };
        }
    }
}
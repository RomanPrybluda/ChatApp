namespace ChatApp.DAL
{
    public class Message
    {
        public int MessageId { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime SentAt { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; } = null!;

        public int UserId { get; set; }

        public User User { get; set; } = null!;

    }
}
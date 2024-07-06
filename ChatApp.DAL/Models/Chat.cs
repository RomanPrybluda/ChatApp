namespace ChatApp.DAL
{
    public class Chat
    {
        public int ChatId { get; set; }

        public string ChatName { get; set; } = null!;

        public ICollection<Message>? Messages { get; set; }

        public ICollection<UserChat>? UserChats { get; set; }

    }
}
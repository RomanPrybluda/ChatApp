namespace ChatApp.DAL
{
    public class Chat
    {
        public int ChatId { get; set; }

        public string ChatName { get; set; } = null!;

        public int CreatorUserId { get; set; }

        public User Creator { get; set; } = null!;

        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public ICollection<UserChat> UserChats { get; set; } = new List<UserChat>();

    }
}
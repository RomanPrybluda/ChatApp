namespace ChatApp.DAL
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public ICollection<UserChat>? UserChats { get; set; }
    }
}
using ChatApp.DAL;

public class ChatInitializer
{
    private readonly ChatAppDbContext _context;

    public ChatInitializer(ChatAppDbContext context)
    {
        _context = context;
    }

    public void InitializeChats()
    {
        int chatCount = _context.Chats.Count();

        if (chatCount < 20)
        {
            int chatsToCreate = 20 - chatCount;

            var chatNames = new List<string>
            {
                "General", "Random", "Sports", "Movies", "Music",
                "Tech", "Gaming", "Books", "Travel", "Food",
                "Fitness", "Health", "Education", "Work", "Finance",
                "Parenting", "Relationships", "Pets", "Hobbies", "Events"
            };

            var userIds = _context.Users.Select(u => u.UserId).ToList();
            var random = new Random();

            for (int i = 0; i < chatsToCreate; i++)
            {
                if (userIds.Count == 0)
                    break;


                int randomUserId = userIds[random.Next(userIds.Count)];

                _context.Chats.Add(new Chat
                {
                    ChatName = chatNames[i % chatNames.Count],
                    CreatorUserId = randomUserId
                });
            }

            _context.SaveChanges();
        }
    }
}

using ChatApp.DAL;

public class UserInitializer
{
    private readonly ChatAppDbContext _context;

    public UserInitializer(ChatAppDbContext context)
    {
        _context = context;
    }

    public void InitializeUsers()
    {
        int userCount = _context.Users.Count();

        if (userCount < 50)
        {
            var userNames = new List<string>
            {
                "Alice", "Bob", "Charlie", "David", "Eve",
                "Frank", "Grace", "Hank", "Ivy", "Jack",
                "Kate", "Leo", "Mona", "Nina", "Oscar",
                "Paul", "Quincy", "Rose", "Sam", "Tina",
                "Uma", "Vince", "Wendy", "Xander", "Yara",
                "Zane", "Anna", "Brian", "Clara", "Derek",
                "Ella", "Fiona", "George", "Holly", "Ian",
                "Jill", "Kevin", "Lara", "Mike", "Nora",
                "Olivia", "Pete", "Quinn", "Rachel", "Steve"
            };

            int usersToCreate = 50 - userCount;

            for (int i = 0; i < usersToCreate; i++)
            {
                _context.Users.Add(new User
                {
                    UserName = userNames[i % userNames.Count]
                });
            }

            _context.SaveChanges();
        }
    }
}

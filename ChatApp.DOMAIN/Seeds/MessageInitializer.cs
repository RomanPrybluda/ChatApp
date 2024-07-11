using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class MessageInitializer
    {
        private readonly ChatAppDbContext _context;

        public MessageInitializer(ChatAppDbContext context)
        {
            _context = context;
        }

        public void InitializeMessages()
        {
            var chats = _context.Chats.ToList();
            var users = _context.Users.ToList();

            if (chats.Any() && users.Any())
            {
                var random = new Random();

                var helloMessages = new List<string>
            {
                "Hello everyone!",
                "Hi there!",
                "Good to see you all!",
                "Greetings!",
                "Welcome to the chat!"
            };

                var categorizedMessages = new Dictionary<string, List<string>>
                {
                    ["General"] = new List<string>
                {
                    "Just checking in.",
                    "Anyone here?",
                    "What's the latest news?",
                    "How's everyone doing?"
                },
                    ["Sports"] = new List<string>
                {
                    "Did you watch the game last night?",
                    "Who's your favorite team?",
                    "Any predictions for the next match?",
                    "Sports events coming up?"
                },
                    ["Movies"] = new List<string>
                {
                    "Have you seen the latest blockbuster?",
                    "What's your favorite movie genre?",
                    "Any movie recommendations?",
                    "Who's your favorite actor?"
                },
                    ["Music"] = new List<string>
                {
                    "What are you listening to?",
                    "Any new albums released?",
                    "Favorite band?",
                    "What's your go-to song?"
                },
                    ["Tech"] = new List<string>
                {
                    "Have you tried the new software update?",
                    "What's the latest in tech?",
                    "Any tech news?",
                    "Favorite gadgets?"
                },
                    ["Gaming"] = new List<string>
                {
                    "What games are you playing?",
                    "Any game recommendations?",
                    "Favorite console?",
                    "Any upcoming game releases?"
                },
                    ["Books"] = new List<string>
                {
                    "What book are you reading?",
                    "Any book recommendations?",
                    "Favorite author?",
                    "Best book you've read recently?"
                },
                    ["Travel"] = new List<string>
                {
                    "Where have you traveled recently?",
                    "Any travel tips?",
                    "Favorite destination?",
                    "Dream vacation spot?"
                },
                    ["Food"] = new List<string>
                {
                    "What's your favorite cuisine?",
                    "Any good recipes?",
                    "Favorite restaurant?",
                    "What's for dinner?"
                },
                    ["Fitness"] = new List<string>
                {
                    "What's your workout routine?",
                    "Any fitness tips?",
                    "Favorite exercise?",
                    "Fitness goals?"
                },
                    ["Health"] = new List<string>
                {
                    "Any health tips?",
                    "How do you stay healthy?",
                    "Favorite healthy snack?",
                    "How's everyone feeling?"
                },
                    ["Education"] = new List<string>
                {
                    "What are you studying?",
                    "Any education resources?",
                    "Favorite subject?",
                    "Any upcoming exams?"
                },
                    ["Work"] = new List<string>
                {
                    "How's work going?",
                    "Any work tips?",
                    "What's your profession?",
                    "Favorite thing about your job?"
                },
                    ["Finance"] = new List<string>
                {
                    "Any finance tips?",
                    "How do you manage your budget?",
                    "Favorite finance book?",
                    "Any investment advice?"
                },
                    ["Parenting"] = new List<string>
                {
                    "Any parenting tips?",
                    "How are the kids?",
                    "Favorite family activity?",
                    "Best parenting advice?"
                },
                    ["Relationships"] = new List<string>
                {
                    "How's everyone doing?",
                    "Any relationship tips?",
                    "Favorite date idea?",
                    "Best relationship advice?"
                },
                    ["Pets"] = new List<string>
                {
                    "How are the pets?",
                    "Any pet tips?",
                    "Favorite pet activity?",
                    "Best pet advice?"
                },
                    ["Hobbies"] = new List<string>
                {
                    "What are your hobbies?",
                    "Any hobby tips?",
                    "Favorite pastime?",
                    "How do you spend your free time?"
                },
                    ["Events"] = new List<string>
                {
                    "Any events coming up?",
                    "Favorite event?",
                    "Any event tips?",
                    "Best event experience?"
                }
                };

                foreach (var chat in chats)
                {
                    int messagesToCreate = random.Next(5, 15);

                    var initialMessages = helloMessages.OrderBy(x => random.Next()).Take(2).ToList();

                    var categoryMessages = categorizedMessages.ContainsKey(chat.ChatName)
                        ? categorizedMessages[chat.ChatName]
                        : new List<string>();
                    initialMessages.AddRange(categoryMessages.OrderBy(x => random.Next()).Take(messagesToCreate - initialMessages.Count));

                    foreach (var messageText in initialMessages)
                    {
                        var randomUser = users[random.Next(users.Count)];

                        _context.Messages.Add(new Message
                        {
                            Text = messageText,
                            SentAt = DateTime.UtcNow,
                            ChatId = chat.ChatId,
                            UserId = randomUser.UserId
                        });
                    }
                }

                _context.SaveChanges();
            }
        }
    }
}
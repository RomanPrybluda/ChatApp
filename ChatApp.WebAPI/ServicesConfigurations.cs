using ChatApp.DAL;
using ChatApp.DOMAIN;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.WebAPI
{
    public static class ServicesConfigurations
    {

        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            ConfigureAppDbContext(builder);
            RegisterServices(builder.Services);
        }

        private static void ConfigureAppDbContext(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ChatAppDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(nameof(ChatAppDbContext)),
                    b => b.MigrationsAssembly("ChatApp.DAL"));
            });
        }

        public static async void DatabaseMigrate(this IServiceProvider serviceProvider)
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ChatAppDbContext>();
                context.Database.Migrate();

                var userInitializer = new UserInitializer(context);
                userInitializer.InitializeUsers();

                var chatInitializer = new ChatInitializer(context);
                chatInitializer.InitializeChats();

                var messageInitializer = new MessageInitializer(context);
                messageInitializer.InitializeMessages();
            }
        }

        private static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers();
            serviceCollection.AddSignalR();
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddSwaggerGen();
            serviceCollection.AddHttpClient();

            serviceCollection.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", builder =>
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed((hosts) => true));
            });

            serviceCollection.AddScoped<UserService>();
            serviceCollection.AddScoped<ChatService>();
            serviceCollection.AddScoped<MessageService>();
        }

        public static void RegisterMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
        }

    }
}
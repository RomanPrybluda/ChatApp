using ChatApp.DAL;
using ChatApp.DOMAIN;
using ChatApp.WebAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<MessageService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatAppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(nameof(ChatAppDbContext)),
        b => b.MigrationsAssembly("ChatApp.DAL"));

});

builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{

    options.AddPolicy("CORSPolicy", builder =>

    builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((hosts) => true));
});

builder.Services.AddSignalR();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CORSPolicy");

app.UseRouting();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.MapHub<ChatHub>("chat-hub");

app.Run();
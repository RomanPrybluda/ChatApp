using ChatApp.DAL;
using ChatApp.DOMAIN;
using ChatApp.WebAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ChatService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ChatAppDbContext)),
        b => b.MigrationsAssembly("ChatApp.DAL"));
});

builder.Logging.AddConsole();

builder.Services.AddSignalR();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ChatAppDbContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chat");


app.Run();

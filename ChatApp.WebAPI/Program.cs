using ChatApp.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.ConfigureServices();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.RegisterMiddleware();

app.MapControllers();

app.MapHub<ChatHub>("/chat-hub");

app.UseCors("CORSPolicy");

app.Run();
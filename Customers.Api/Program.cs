using Customers.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpoints(typeof(Program).Assembly);

var app = builder.Build();

app.MapEndpoints();
app.Run();
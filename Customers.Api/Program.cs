using Customers.Api.Data;
using Customers.Api.Extensions;
using Customers.DAL.Abstractions;
using Customers.DAL.Data;
using Customers.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpoints(typeof(Program).Assembly);

var connString = builder.Configuration.GetConnectionString(name: "Customers");
builder.Services.AddSqlite<CustomersContext>(connectionString: connString);
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

app.MapEndpoints();
await app.MigrateDbAsync();

app.Run();
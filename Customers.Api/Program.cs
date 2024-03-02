using Customers.Api.Abstractions;
using Customers.Api.Extensions;
using Customers.Api.Services;
using Customers.DAL.Abstractions;
using Customers.DAL.Data;
using Customers.DAL.Data.InitDataFactory;
using Customers.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpoints(typeof(Program).Assembly);

var connString = builder.Configuration.GetConnectionString(name: "Customers");

// DAL dependencies
builder.Services.AddSqlite<CustomersContext>(connectionString: connString);
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<AbstractDataFactory, DataFactory>();

// Events dependencies
builder.Services.AddSingleton<IEventService, EventService>();

var app = builder.Build();

app.MapEndpoints();
await app.MigrateDbAsync();

app.Run();
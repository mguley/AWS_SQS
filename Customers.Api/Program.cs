using Amazon.SQS;
using Customers.Api.Abstractions;
using Customers.Api.Extensions;
using Customers.Api.Services;
using Customers.DAL.Abstractions;
using Customers.DAL.Data;
using Customers.DAL.Data.InitDataFactory;
using Customers.DAL.Repositories;
using Customers.Messaging.Abstractions;
using Customers.Messaging.Models;
using Customers.Messaging.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpoints(typeof(Program).Assembly);

var connString = builder.Configuration.GetConnectionString(name: "Customers");

// DAL dependencies
builder.Services.AddSqlite<CustomersContext>(connectionString: connString);
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<AbstractDataFactory, DataFactory>();

// Events dependencies
builder.Services.AddSingleton<IEventService, EventService>();

// AWS queue configuration
builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection(QueueSettings.Key));
builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
builder.Services.AddSingleton<ISqsMessenger, SqsMessenger>();

var app = builder.Build();

app.MapEndpoints();
await app.MigrateDbAsync();

app.Run();
using MessageHandler.Runtime;
using MessageHandler.EventSourcing;
using MessageHandler.EventSourcing.AzureTableStorage;
using MessageHandler.Quickstart.EventSourcing.Projection;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
                 .AddEnvironmentVariables()
                 .AddUserSecrets<Program>()
                 .Build();

var eventSourceTableName = "OrderBooking";
var eventSourceStreamTypeName = "OrderBooking";
var handlerName = "orderbooking";

var connectionString = configuration.GetValue<string>("azurestoragedata")
                                       ?? throw new Exception("No 'azurestoragedata' connection string was provided. Use User Secrets or specify via environment variable.");

// Add services to the container.
builder.Services.AddSingleton<IPurchaseOrdersRegistry, PurchaseOrdersRegistry>();
builder.Services.AddSingleton<IHostedService, RestoreOnStartup>();
builder.Services.AddMessageHandler(handlerName, runtimeConfiguration =>
{
    runtimeConfiguration.EventSourcing(source =>
    {
        source.Stream(eventSourceStreamTypeName,
            from => from.AzureTableStorage(connectionString, eventSourceTableName),
            into =>
            {
                into.Projection<ProjectInMemory>();
            });
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Contracts;
using Microsoft.EntityFrameworkCore;
using OfferteTool;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.ErrorHandling;
using Wolverine.FluentValidation;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;
using Wolverine.RabbitMQ;
using Wolverine.SqlServer;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Wolverine");
if (string.IsNullOrWhiteSpace(connectionString)) throw new InvalidOperationException("No connection string found.");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseWolverine(opts =>
{
    opts.PersistMessagesWithSqlServer(connectionString);
    opts.UseEntityFrameworkCoreTransactions();
    
    opts.UseFluentValidation();

    opts.Policies.AutoApplyTransactions();
    opts.Policies.UseDurableLocalQueues();
    
    opts.OnAnyException()
        .RetryWithCooldown(
            TimeSpan.FromSeconds(3),
            TimeSpan.FromSeconds(9),
            TimeSpan.FromSeconds(27));
    
    opts.PublishMessage<OfferteCreatedIntegrationEvent>()
        .ToRabbitExchange("offertes");

    opts.UseRabbitMq().AutoProvision();
});

builder.Services.AddSingleton<MailService>();

builder.Services.AddProblemDetails();

builder.Services.AddDbContextWithWolverineIntegration<OfferteContext>(opts => 
    opts.UseSqlServer(connectionString));

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.MapWolverineEndpoints(opts =>
    opts.UseFluentValidationProblemDetailMiddleware());

var context = app.Services.GetRequiredService<OfferteContext>();
await context.Database.MigrateAsync();

await app.RunAsync();
using Wolverine;
using Wolverine.RabbitMQ;

var builder = Host.CreateDefaultBuilder(args);

await builder
    .UseWolverine(opts =>
    {
        opts.ListenToRabbitQueue("offertes");
        opts.UseRabbitMq()
            .DeclareExchange("offertes", ex => ex.BindQueue("offertes"))
            .AutoProvision();
    })
    .Build()
    .RunAsync();
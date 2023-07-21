using Wolverine;
using Wolverine.RabbitMQ;

var builder = Host.CreateDefaultBuilder(args);

await builder
    .UseWolverine(opts =>
    {
        opts.ListenToRabbitQueue("offers");
        opts.UseRabbitMq()
            .DeclareExchange("offers", ex => ex.BindQueue("offers"))
            .AutoProvision();
    })
    .Build()
    .RunAsync();
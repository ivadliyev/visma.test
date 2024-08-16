using Microsoft.EntityFrameworkCore;
using visma.test.broker;
using visma.test.broker.Models.Dtos;
using visma.test.broker.Repositories.Channel;
using visma.test.broker.Repositories.Message;
using visma.test.broker.Repositories.Subscription;
using visma.test.broker.Services.Channel;
using visma.test.broker.Services.Message;
using visma.test.broker.Services.Subscription;
using visma.test.broker.Services.SubscriptionMessage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add context
builder.Services.AddDbContext<BrokerDbContext>(opt => opt.UseSqlite("Data Source=broker.db"));

//add repos
builder.Services.AddScoped<IChannelRepository, ChannelRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

//add services
builder.Services.AddScoped<IChannelService, ChannelService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISubscriptionMessageService, SubscriptionMessageService>();

//automapper
builder.Services.AddAutoMapper(typeof(AutomapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//routes
app.MapGet("api/channels", async (IChannelService channelService) =>
{
    return await channelService.GetAll();
})
.WithName("GetAllChannels")
.WithOpenApi();
app.MapPost("api/channels", async (ChannelCreateDto model, IChannelService channelService) =>
{
    var channel =  await channelService.Create(model);
    return Results.Created($"api/channels/{channel.Id}", channel);
})
.WithName("CreateChannel")
.WithOpenApi();

app.MapPost("api/channels/{channelId}/messages", async (int channelId, MessageCreateDto model, IMessageService messageService) =>
{
    var messages =  await messageService.CreateMany(channelId, model);
    return Results.Created($"api/channels/{channelId}/messages", messages);
})
.WithName("CreateMessage")
.WithOpenApi();

app.MapPost("api/channels/{channelId}/subscriptions", async (int channelId, ISubscriptionService subscriptionService) =>
{
    var subscription =  await subscriptionService.Create(channelId);
    return Results.Created($"api/channels/{channelId}/subscriptions/{subscription.Id}", subscription);
})
.WithName("CreateSubscription")
.WithOpenApi();

app.MapDelete("api/subscriptions/{id}", async (int id, ISubscriptionMessageService subscriptionMessageService) =>
{
    await subscriptionMessageService.Delete(id);
    return Results.Ok();
})
.WithName("DeleteSubscription")
.WithOpenApi();

app.MapGet("api/subscriptions/{subscriptionId}/messages", async (int subscriptionId, IMessageService messageService) =>
{
    return await messageService.ListNotReceivedBySubscriptionId(subscriptionId);
})
.WithName("GetMessages")
.WithOpenApi();

app.MapPatch("api/messages/{id}", async (int id, MessageEditDto model, IMessageService messageService) =>
{
    return await messageService.Update(id, model);
})
.WithName("UpdateMessageStatus")
.WithOpenApi();

app.Run();
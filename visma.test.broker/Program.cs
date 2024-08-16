using Microsoft.EntityFrameworkCore;
using visma.test.broker;
using visma.test.broker.Models.Dtos;
using visma.test.broker.Repositories.Channel;
using visma.test.broker.Repositories.Message;
using visma.test.broker.Services.Channel;

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

//add services
builder.Services.AddScoped<IChannelService, ChannelService>();

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

app.Run();
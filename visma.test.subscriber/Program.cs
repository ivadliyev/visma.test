// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;
using visma.test.subscriber.Models.Dto;

Console.WriteLine("Please enter channel id");
int channelId = int.Parse(Console.ReadLine() ?? "");

var httpClient = new HttpClient();
var subscription = await Subscribe(httpClient, channelId);

RunLoop(httpClient, subscription.Id);

Console.ReadLine();

static async void RunLoop(HttpClient client, int subscriptionId) 
{
    Random rnd = new();
    var timeout = rnd.Next(1, 5);
    Console.WriteLine($"Tiemout {timeout} seconds");
  
    while(true) {
        var messages = await FetchMessages(client, subscriptionId);
        if (messages.Count > 0)
        {
            messages.ForEach(async m => {
                Console.WriteLine($"Message {m.Id} with body: {m.Body}");
                await MarkMessageAsRead(client, m.Id);
            });
        }
        Thread.Sleep(timeout * 1000);
    }
}

static async Task<SubscriptionDto> Subscribe(HttpClient client, int channelId)
{
    var response = await client.PostAsync($"http://localhost:5067/api/channels/{channelId}/subscriptions", null);
    return await response.Content.ReadFromJsonAsync<SubscriptionDto>() ?? throw new NullReferenceException("Subscription error");
}

static async Task<List<MessageDto>> FetchMessages(HttpClient client, int subscriptionId)
{
    return await client.GetFromJsonAsync<List<MessageDto>>($"http://localhost:5067/api/subscriptions/{subscriptionId}/messages") ?? [];
}

static async Task MarkMessageAsRead(HttpClient client, int messageId)
{
    var response = await client.PatchAsJsonAsync($"http://localhost:5067/api/messages/{messageId}", new {status=2});
    Console.WriteLine($"Message {messageId} marked as read");
}
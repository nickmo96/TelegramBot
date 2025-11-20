using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TelegramBot.Model;

namespace TelegramBot ;

public class TelegramBot
{
    private readonly string Token = "8393432003:AAFCByo9c06ZvAd2U1SHvDo-bq-h26sp-M8";
    private HttpClient _client;
    private Dictionary<string, Func<string, Task>> _commands;

    private long? _offset;

    public TelegramBot()
    {
        _client = new HttpClient();

    //      _commands = new Dictionary<string, Func<string, Task>>
    // {
    //     { "/help", HelpCommandAsync },
       
    // };
    }

    public async Task<string> GetUpdatesAsync()
{
    var request = new HttpRequestMessage(
        HttpMethod.Get,
        $"https://api.telegram.org/bot{Token}/getUpdates"
    );

    var response = await _client.SendAsync(request);
    response.EnsureSuccessStatusCode();

    string jsonData = await response.Content.ReadAsStringAsync();
    return jsonData;
}



    public async Task<string> GetUpdatesOffSetAsync()
    {
        string url = $"https://api.telegram.org/bot{Token}/getUpdates?offset={_offset}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await _client.SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();

        return content;

    }
     public async Task<List<JToken>> GetUpdatesJObjectAsync()
    {
        string content = await GetUpdatesOffSetAsync();
        JObject json = JObject.Parse(content);
        List<JToken?> messages = json["result"].ToList();    
        return messages;  
    }

public async Task SendMessageAsync(string text, string chatId)
{
    var url = $"https://api.telegram.org/bot{Token}/sendMessage?chat_id={chatId}&text={Uri.EscapeDataString(text)}";
    await _client.GetAsync(url);
}

public async Task SendMessageAsync2(string text, string chatID){
var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.telegram.org/bot{Token}/sendMessage?chat_id={chatID}&text={text}");


}
    public async Task CommandListenerAsync()
{
    Console.WriteLine("Listening...");
    bool listening = true;

    while (listening)
    {
        List<JToken>? messages = await GetUpdatesJObjectAsync();

        if (messages != null && messages.Count > 0)
        {
            foreach (JToken item in messages)
            {
                JToken? message = item["message"];
                if (message == null) continue;

                long updateId = item.Value<long>("update_id");
                string? messageText = message["text"]?.ToString();
                string? entity = message["entities"]?[0]?["type"]?.ToString();
                string chatId = message["chat"]?["id"]?.ToString() ?? "0";

                if (messageText.StartsWith("/"))
                {
                    switch (messageText.ToLower())
                    {
                        case "/hej":
                            await SendMessageAsync("Hej! Hvordan går det?", chatId);
                            break;

                        case "/help":
                            await SendMessageAsync2(
                                "Kommandoer:\n" +
                                "/hej - Bot siger hej\n" +
                                "/nadia - Spammer Nadia er smuk\n" +
                                "/temp - Viser temperatur i Kbh\n" +
                                "/help - Viser denne besked", chatId
                            );
                            break;

                        case "/nadia":
                            for (int i = 0; i < 10; i++)
                                await SendMessageAsync("Nadia er smuk", chatId);
                            break;

                        case "/temp":
                           

                        default:
                            await SendMessageAsync("Ukendt kommando. Prøv /help", chatId);
                            break;
                    }

                    Console.WriteLine($"Ny kommando modtaget: {messageText} fra chat ID: {chatId}");
                }
                else
                {
                    Console.WriteLine($"Ny besked modtaget: {messageText} fra chat ID: {chatId}");
                }

                _offset = updateId + 1;
            }
        }

        await Task.Delay(1000);
    
    }
}
    



    



}

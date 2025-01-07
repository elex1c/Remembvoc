using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.Infrastructure.APIs.Helpers;
using Remembvoc.Infrastructure.APIs.Models.GroqModels;

namespace Remembvoc.Infrastructure.APIs.Gens;

public class LIamaGen : ISentenceGen
{
    private readonly GroqHelper _helper;
    private string API_KEY => _helper.ApiKey;
    private string ENDPOINT => _helper.Endpoint;
    private string MODEL => _helper.Model;
    
    public LIamaGen(GroqHelper helper)
    {
        _helper = helper;
    }
    
    public async Task<string?> GenerateSentence(string word, string language)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_KEY);
            
            Request request = new()
            {
                messages = [new Message { content = $"Create me a sentence with word \"{word}\" in language \"{language}\". In response put generated sentence between pipe characters. If you have some problems with generating, please, just sent 'ERROR'.", role = "user" }],
                model = MODEL,
                max_tokens = 1024,
                temperature = 1,
                top_p = 1,
                stream = true,
                stop = null
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(ENDPOINT, content);
            
            if (response.IsSuccessStatusCode)
            {
                string? resp = await response.Content.ReadAsStringAsync();

                string? sentence = _helper.GetSentenceFromStringResponse(resp);

                return sentence;
            }

            return null;
        }
    }
}
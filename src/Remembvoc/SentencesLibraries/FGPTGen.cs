using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Remembvoc.Models;
using Remembvoc.Models.FGPT;

namespace Remembvoc.SentencesLibraries;

public class FGPTGen : ISentenceGen
{
    private const string API_KEY = "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";
    private const string ENDPOINT = "https://api-inference.huggingface.co/models/meta-llama/Llama-3.2-1B-Instruct/v1/chat/completions";
    private const string MODEL = "meta-llama/Llama-3.2-1B-Instruct";

    public async Task<string?> Generate(string word, Languages language)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");

            Request request = new()
            {
                messages = [new Message { content = $"Create me a sentence with word \"{word}\" in language \"{language}\"", role = "user" }],
                model = MODEL,
                max_tokens = 500,
                stream = false
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(ENDPOINT, content);

            if (response.IsSuccessStatusCode)
            {
                string? resp = await response.Content.ReadAsStringAsync();

                // TODO: Create response model
            }

            return null;
        }
    }
}
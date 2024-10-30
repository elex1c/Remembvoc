using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Remembvoc.Models;
using Remembvoc.Models.FGPT;

namespace Remembvoc.SentencesLibraries;

public class LIamaGen : ISentenceGen
{
    private const string API_KEY = "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";
    private const string ENDPOINT = "https://api.groq.com/openai/v1/chat/completions";
    private const string MODEL = "llama3-8b-8192";

    public async Task<string?> GenerateSentence(string word, Languages language)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");

            Request request = new()
            {
                messages = [new Request.Message { content = $"Create me a sentence with word \"{word}\" in language \"{language}\". In response put generated sentence between pipe characters. If you have some problems with generating, please, just sent 'ERROR'.", role = "user" }],
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

                string? sentence = GetSentenceFromStringResponse(resp);

                return sentence;
            }

            return null;
        }
    }

    private string? GetSentenceFromStringResponse(string content)
    {
        string sentence = "";
        
        foreach (string s in content.Split("data: "))
        {
            try
            {
                sentence += JsonSerializer.Deserialize<Response>(s.TrimEnd('\n'))?
                    .choices[0]
                    .delta
                    .content ?? "";
            }
            catch (Exception) { continue; }
        }

        sentence = sentence.Split('|')[1]
            .Trim();
        
        return sentence == string.Empty ? null : sentence;
    }
}
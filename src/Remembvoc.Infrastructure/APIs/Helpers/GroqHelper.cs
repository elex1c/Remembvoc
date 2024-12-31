using System.Text.Json;
using Remembvoc.Infrastructure.APIs.Models.GroqModels;

namespace Remembvoc.Infrastructure.APIs.Helpers;

public class GroqHelper
{
    public const string ENDPOINT = "https://api.groq.com/openai/v1/chat/completions";
    public const string MODEL = "llama3-8b-8192";
    
    public readonly string API_KEY;
    
    public GroqHelper(string apiKey)
    {
        API_KEY = apiKey;
    }
    
    public string? GetSentenceFromStringResponse(string content)
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
using System.Text.Json;
using Microsoft.Extensions.Options;
using Remembvoc.ApplicationCore.Common.Settings;
using Remembvoc.Infrastructure.APIs.Models.GroqModels;

namespace Remembvoc.Infrastructure.APIs.Helpers;

public class GroqHelper
{
    private readonly ApiSettings _options;
    public string ApiKey => _options.LIamaApiKey;
    public string Endpoint => _options.Endpoint;
    public string Model => _options.Model;
    
    public GroqHelper(IOptions<ApiSettings> options)
    {
        _options = options.Value;
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

        if (sentence.Split('|').Length > 1)
            sentence = sentence.Split('|')[1].Trim();
        
        return sentence == string.Empty ? null : sentence;
    }
}
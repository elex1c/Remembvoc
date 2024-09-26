using System.Net.Http;
using Remembvoc.Models;

namespace Remembvoc.SentencesLibraries;

public class CopilotGen : ISentenceGen
{
    public string API_KEY { get; set; }
    
    public string Generate(string word, Languages language)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("X-API-KEY", API_KEY);
        }
    }
}
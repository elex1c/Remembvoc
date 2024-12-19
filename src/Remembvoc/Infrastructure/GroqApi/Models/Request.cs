namespace Remembvoc.Infrastructure.GroqApi.Models;

public class Request
{
    public Message[] messages { get; set; }
    public string model { get; set; }
    public int temperature { get; set; }
    public int max_tokens { get; set; }
    public int top_p { get; set; }
    public bool stream { get; set; }
    public object? stop { get; set; }
    
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}


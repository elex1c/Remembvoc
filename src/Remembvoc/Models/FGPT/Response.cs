namespace Remembvoc.Models.FGPT;

public class Response
{
    public Choices[] choices { get; set; }
    public class Choices
    {
        public Delta delta { get; set; }
    }

    public class Delta
    {
        public string content { get; set; }
    }
}
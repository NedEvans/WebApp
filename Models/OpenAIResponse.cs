namespace WebApp
{
    public class ResponseBody
    {
        public List<Choice> Choices { get; set; }
    }
    public class Message
    {
        public string content { get; set; }
    }
    public class Choice
    {
        public Message Message { get; set; }
    }

}

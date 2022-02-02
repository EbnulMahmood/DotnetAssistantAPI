namespace AssistantAPI.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; } = null;
    }
}
namespace Assistant.Data.Models
{
    internal class Task
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}

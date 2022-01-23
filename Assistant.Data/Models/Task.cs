namespace Assistant.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime DueDate { get; set; }
        public Person? RequestedBy { get; set; }
        public Person? AssignedTo { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
using AssistantAPI.Models;
using Microsoft.EntityFrameworkCore;
using Task = AssistantAPI.Models.Task;

namespace AssistantAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Person>? Persons { get; set; }
        public DbSet<Task>? Tasks { get; set; }
    }
}

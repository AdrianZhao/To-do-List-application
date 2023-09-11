using Microsoft.EntityFrameworkCore;
using To_do_List_application.Models;

namespace To_do_List_application.Data
{
    public class ToDoListApplicationDbContext : DbContext
    {
        public ToDoListApplicationDbContext(DbContextOptions<ToDoListApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ToDoItem> Item { get; set; } = default!;
        public DbSet<ToDoList> List { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<ToDoItem>().HasOne(i => i.List).WithMany(l => l.Items).HasForeignKey(i => i.ToDoListID).OnDelete(DeleteBehavior.Cascade);
        }        
    }    
}

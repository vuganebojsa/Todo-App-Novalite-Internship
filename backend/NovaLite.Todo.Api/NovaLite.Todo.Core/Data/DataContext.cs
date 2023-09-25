using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Core.Data
{
    public class DataContext : DbContext
    {
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<TodoItem> ToDoItems { get; set; }
        public DbSet<TodoReminder> Reminders { get; set; }
        public DbSet<TodoAttachment> Attachments { get; set; }
        public DbSet<TodoUser> Users { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildTodoList(modelBuilder);
            BuildTodoUser(modelBuilder);
            BuildTodoItem(modelBuilder);
            BuildTodoReminder(modelBuilder);
            BuildTodoAttachment(modelBuilder);
        }

        private static void BuildTodoAttachment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoAttachment>()
               .HasKey(a => a.Id);

            modelBuilder.Entity<TodoAttachment>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<TodoAttachment>().Property(tdl => tdl.FileName).HasMaxLength(255);
        }

        private static void BuildTodoReminder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoReminder>()
               .HasKey(a => a.Id);
            modelBuilder.Entity<TodoReminder>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
        }

        private static void BuildTodoItem(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                           .HasKey(a => a.Id);
            modelBuilder.Entity<TodoItem>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
        }

        private static void BuildTodoUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoUser>()
                            .HasMany(td => td.ToDoLists)
                            .WithOne(td => td.TodoUser)
                            .HasForeignKey(td => td.TodoUserId);
            modelBuilder.Entity<TodoUser>().Property(tdl => tdl.Email).HasMaxLength(255);
            modelBuilder.Entity<TodoUser>()
               .HasKey(a => a.Id);
            modelBuilder.Entity<TodoUser>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
        }

        private static void BuildTodoList(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoList>()
                .HasMany(td => td.TodoItems)
                .WithOne(td => td.TodoList)
                .HasForeignKey(td => td.TodoListId);
            modelBuilder.Entity<ToDoList>()
                .HasMany(td => td.Reminders)
                .WithOne(td => td.TodoList)
                .HasForeignKey(td => td.TodoListId);
            modelBuilder.Entity<ToDoList>()
               .HasMany(td => td.Attachments)
               .WithOne(td => td.TodoList)
               .HasForeignKey(td => td.TodoListId);
            modelBuilder.Entity<ToDoList>().HasKey(tdl => tdl.Id);
            modelBuilder.Entity<ToDoList>()
                .Property(tdl => tdl.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<ToDoList>().Property(tdl => tdl.Title).HasMaxLength(255);
        }
    }
}

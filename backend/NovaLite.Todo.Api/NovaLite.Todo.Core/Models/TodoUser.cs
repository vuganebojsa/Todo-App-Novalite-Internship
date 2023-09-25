namespace NovaLite.Todo.Core.Models
{
    public enum Role
    {
        User, Admin
    }
    public class TodoUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        public ICollection<ToDoList> ToDoLists { get; set; } = new List<ToDoList>();

        public TodoUser(string email)
        {
            Email = email;

        }
        public TodoUser(string email, Role role, List<ToDoList> todos)
        {
            Email = email;
            Role = role;
            ToDoLists = todos;
        }
    }
}

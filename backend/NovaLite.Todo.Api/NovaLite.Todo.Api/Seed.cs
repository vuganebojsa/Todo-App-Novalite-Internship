
using NovaLite.Todo.Core.Data;
using NovaLite.Todo.Core.Models;

namespace Novalite.Todo.Api
{
    public class Seed
    {
        private readonly DataContext _dataContext;
        public Seed(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void SeedDataContext()
        {
            if (!_dataContext.Users.Any())
            {
                AddUsers();
                _dataContext.SaveChanges();

            }
            if (!_dataContext.ToDoLists.Any())
            {
                AddTodoLists();
                _dataContext.SaveChanges();

            }
            if (!_dataContext.ToDoItems.Any())
            {
                AddToDoItems();
                _dataContext.SaveChanges();
            }


        }

        private void AddUsers()
        {
            var users = new List<TodoUser>()
                {
                    new TodoUser("nebojsavuga01@gmail.com", Role.Admin, new List<ToDoList>()),   
                    new TodoUser("nebojsavuga@gmail.com", Role.User, new List<ToDoList>())
                };

            _dataContext.Users.AddRange(users);


        }

        private void AddTodoLists()
        {
            var user = _dataContext.Users.FirstOrDefault();
            var todos = new List<ToDoList>()
                {
                    new ToDoList("Wake Up Routine", "What i need to do when i wake up", new List<TodoItem>(), new List<TodoReminder>(), new List<TodoAttachment>()),
                    new ToDoList("Test Second", "Testing the second list", new List<TodoItem>(), new List<TodoReminder>(), new List<TodoAttachment>()),
                };
            todos.ElementAt(0).TodoUserId = user.Id;
            todos.ElementAt(1).TodoUserId = user.Id;
            _dataContext.ToDoLists.AddRange(todos);


        }

        private void AddToDoItems()
        {
            var todoList = _dataContext.ToDoLists.FirstOrDefault();
            var todos = new List<TodoItem>()
            {
                new TodoItem("I need to get out of the bed", Status.Open),
                new TodoItem("I need to brush my teeth", Status.Open)
            };
            todos.ElementAt(0).TodoList = todoList;
            todos.ElementAt(0).TodoListId = todoList.Id;
            todos.ElementAt(1).TodoList = todoList;
            todos.ElementAt(1).TodoListId = todoList.Id;
            _dataContext.ToDoItems.AddRange(todos);
            _dataContext.SaveChanges();
            todoList.TodoItems = _dataContext.ToDoItems.ToList();
            _dataContext.SaveChanges();
        }

    }
}
using ToDoList.API.Models;

namespace ToDoList.API.Services;

public interface ITodoService
{
    Task<List<TodoItem>> GetAllTodoItemsAsync();

    Task<TodoItem?> GetTodoItemByIdAsync(int id);

    Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem);

    Task UpdateTodoItemAsync(TodoItem todoItem);

    Task DeleteTodoItemAsync(int id);
}

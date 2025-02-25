using Microsoft.EntityFrameworkCore;
using ToDoList.API.Models;

namespace ToDoList.API.Services;

public class TodoService(ApplicationDbContext context) : ITodoService
{
    public async Task<List<TodoItem>> GetAllTodoItemsAsync()
    {
        return await context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem?> GetTodoItemByIdAsync(int id)
    {
        return await context.TodoItems.FindAsync(id);
    }

    public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
    {
        context.TodoItems.Add(todoItem);
        await context.SaveChangesAsync();

        return todoItem;
    }

    public async Task UpdateTodoItemAsync(TodoItem todoItem)
    {
        var existingItem = await context.TodoItems.FindAsync(todoItem.Id)
            ?? throw new KeyNotFoundException("Item not found.");

        context.Entry(existingItem).CurrentValues.SetValues(todoItem);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTodoItemAsync(int id)
    {
        var todoItem = await context.TodoItems.FindAsync(id)
            ?? throw new KeyNotFoundException("No item found with the specified ID.");

        context.TodoItems.Remove(todoItem);
        await context.SaveChangesAsync();
    }
}

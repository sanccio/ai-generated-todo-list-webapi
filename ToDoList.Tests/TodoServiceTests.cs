using Microsoft.EntityFrameworkCore;
using ToDoList.API;
using ToDoList.API.Models;
using ToDoList.API.Services;

namespace ToDoList.Tests;

[TestFixture]
public class TodoServiceTests
{
    private ApplicationDbContext _context;
    private TodoService _todoService;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TodoListTest")
            .Options;

        _context = new ApplicationDbContext(options);

        _context.Database.EnsureDeleted();

        _context.TodoItems.Add(new TodoItem { Id = 1, Title = "Test Task 1", Description = "Description 1" });
        _context.TodoItems.Add(new TodoItem { Id = 2, Title = "Test Task 2", Description = "Description 2" });
        _context.SaveChanges();

        _todoService = new TodoService(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task GetAllTodoItemsAsync_ReturnsAllItems()
    {
        var items = await _todoService.GetAllTodoItemsAsync();

        Assert.That(items, Has.Count.EqualTo(2));
        Assert.That(items[0].Title, Is.EqualTo("Test Task 1"));
    }

    [Test]
    public async Task GetTodoItemByIdAsync_ReturnsItemIfItExists()
    {
        var item = await _todoService.GetTodoItemByIdAsync(1);

        Assert.That(item, Is.Not.Null);
        Assert.That(item.Title, Is.EqualTo("Test Task 1"));
    }

    [Test]
    public async Task GetTodoItemByIdAsync_ReturnsNullIfItDoesNotExist()
    {
        var item = await _todoService.GetTodoItemByIdAsync(3);

        Assert.That(item, Is.Null);
    }

    [Test]
    public async Task CreateTodoItemAsync_AddsNewItem()
    {
        var newItem = new TodoItem { Title = "New Task", Description = "New Description" };

        await _todoService.CreateTodoItemAsync(newItem);

        var item = await _context.TodoItems.FindAsync(newItem.Id);
        Assert.That(item, Is.Not.Null);
        Assert.That(item.Title, Is.EqualTo("New Task"));
    }

    [Test]
    public async Task UpdateTodoItemAsync_UpdatesExistingItem()
    {
        var updatedItem = new TodoItem { Id = 1, Title = "Updated Task", Description = "Updated Description" };

        await _todoService.UpdateTodoItemAsync(updatedItem);

        var item = await _context.TodoItems.FindAsync(1);
        Assert.That(item, Is.Not.Null);
        Assert.That(item.Title, Is.EqualTo("Updated Task"));
    }

    [Test]
    public async Task DeleteTodoItemAsync_DeletesItem()
    {
        await _todoService.DeleteTodoItemAsync(1);

        var item = await _context.TodoItems.FindAsync(1);
        Assert.That(item, Is.Null);
    }

    [Test]
    public void UpdateTodoItemAsync_ThrowsKeyNotFoundException_IfItemDoesNotExist()
    {
        var nonExistentItem = new TodoItem { Id = 999, Title = "Nonexistent", Description = "This task does not exist in DB" };

        Assert.ThrowsAsync<KeyNotFoundException>(() => _todoService.UpdateTodoItemAsync(nonExistentItem));
    }

    [Test]
    public void DeleteTodoItemAsync_ThrowsKeyNotFoundException_IfItemDoesNotExist()
    {
        var nonExistentId = 999;

        Assert.ThrowsAsync<KeyNotFoundException>(() => _todoService.DeleteTodoItemAsync(nonExistentId));
    }
}
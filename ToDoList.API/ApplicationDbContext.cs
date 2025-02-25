namespace ToDoList.API;

using Microsoft.EntityFrameworkCore;
using ToDoList.API.Models;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public virtual DbSet<TodoItem> TodoItems { get; set; }
}
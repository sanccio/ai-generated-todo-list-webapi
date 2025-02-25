using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Models;
using ToDoList.API.Services;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(ITodoService todoService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return Ok(await todoService.GetAllTodoItemsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await todoService.GetTodoItemByIdAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var createdTodoItem = await todoService.CreateTodoItemAsync(todoItem);

            return CreatedAtAction(nameof(GetTodoItem), new { id = createdTodoItem.Id }, createdTodoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest("Item ID mismatch");
            }

            try
            {
                await todoService.UpdateTodoItemAsync(todoItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            try
            {
                await todoService.DeleteTodoItemAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
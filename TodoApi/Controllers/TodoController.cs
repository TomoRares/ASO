using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly AppDbContext _context;

    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    // CREATE (Post)
    [HttpPost]
    public async Task<ActionResult<TodoItem>> CreateTodo(TodoItem item)
    {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTodoById), new { id = item.Id }, item);
    }

    // READ ALL (Get)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAllTodos()
    {
        return await _context.TodoItems.ToListAsync();
    }

    // READ BY ID (Get)
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodoById(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    // UPDATE (Put)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, TodoItem item)
    {
        if (id != item.Id) return BadRequest();

        _context.Entry(item).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.TodoItems.Any(e => e.Id == id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item == null) return NotFound();

        _context.TodoItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
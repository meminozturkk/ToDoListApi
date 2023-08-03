using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using ToDoListApi.Models;

namespace ToDoListApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToDoController : ControllerBase
	{
		private readonly ToDoDbContext toDoDbContext;

		public ToDoController(ToDoDbContext toDoDbContext)
		{
			this.toDoDbContext = toDoDbContext;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllToDoes()
		{
			var todos = await toDoDbContext.Todos.ToListAsync();

			return Ok(todos);
		}
		[HttpPost]
		[Route("addtodo")]
		public async Task<IActionResult> AddToDo(ToDo toDo)
		{
			toDo.Id = Guid.NewGuid();
			toDo.IsCompleted = false;
			toDoDbContext.Add(toDo);
			await toDoDbContext.SaveChangesAsync();
			return Ok(toDo);
		}

		[HttpPut]
		[Route("updatetodo/{id}")]
		public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, ToDo updateToDo)
		{
			var toDo = await toDoDbContext.Todos.FindAsync(id);

			if (toDo == null)
			{
				return NotFound();
			}

			toDo.Title = updateToDo.Title;
			toDo.Detail = updateToDo.Detail;
			toDo.Priority = updateToDo.Priority;
			toDo.IsCompleted = updateToDo.IsCompleted;
			toDoDbContext.Update(toDo);
			await toDoDbContext.SaveChangesAsync();
			return Ok(toDo);
		}
		[HttpDelete]
		[Route("deletetodo/{id}")]
		public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
		{
			var todo = await toDoDbContext.Todos.FindAsync(id);
			if (todo == null)
			{
				return NotFound();
			}
			toDoDbContext.Remove(todo);
			await toDoDbContext.SaveChangesAsync();
			return Ok(todo);
		}
	}
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using ToDoListApi.Models;
using static ToDoListApi.Helpers.TodoHelper;

namespace ToDoListApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToDoController : ControllerBase
	{
		private readonly ToDoDbContext _toDoDbContext;

		public ToDoController(ToDoDbContext toDoDbContext)
		{
			_toDoDbContext = toDoDbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllToDoes()
		{
			var todos = await ToDoHelper.GetSortedTodosForUser(_toDoDbContext, UserController.user.Id);

			return Ok(todos);
		}

		[HttpGet("GetTodoDetails/{id}")]
		public async Task<ActionResult<ToDo>> GetTodoDetails(Guid id)
		{
			var todo = await ToDoHelper.GetToDoById(_toDoDbContext, id);

			if (todo == null)
			{
				return NotFound();
			}

			return todo;
		}

		[HttpPost("addtodo")]
		public async Task<IActionResult> AddToDo(ToDo toDo)
		{
			var result = await ToDoHelper.AddToDoForUser(_toDoDbContext, UserController.user, toDo);

			if (result.Success)
			{
				return Ok(result.Entity);
			}
			else
			{
				return BadRequest(result.ErrorMessage);
			}
		}

		[HttpPut("updatetodo/{id}")]
		public async Task<IActionResult> UpdateTodo(Guid id, ToDo updateToDo)
		{
			var result = await ToDoHelper.UpdateToDo(_toDoDbContext, id, updateToDo);

			if (result.Success)
			{
				return Ok(result.Entity);
			}
			else
			{
				return NotFound();
			}
		}

		[HttpDelete("deletetodo/{id}")]
		public async Task<IActionResult> DeleteTodo(Guid id)
		{
			var result = await ToDoHelper.DeleteToDo(_toDoDbContext, id);

			if (result.Success)
			{
				return Ok(result.Entity);
			}
			else
			{
				return NotFound();
			}
		}
	}
}

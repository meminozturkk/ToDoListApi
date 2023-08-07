using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using ToDoListApi.Models;

namespace ToDoListApi.Helpers
{
	public class TodoHelper
	{
		public static class ToDoHelper
		{
			public static async Task<List<ToDo>> GetSortedTodosForUser(ToDoDbContext context, Guid userId)
			{
				var todos = await context.Todos.Where(t => t.UserId == userId).ToListAsync();

				return todos.OrderBy(t => t.Priority switch
				{
					"High" => 0,
					"Medium" => 1,
					"Low" => 2,
					_ => 3
				}).ToList();
			}

			public static async Task<ToDo> GetToDoById(ToDoDbContext context, Guid id)
			{
				return await context.Todos.FindAsync(id);
			}

			public static async Task<(bool Success, ToDo Entity, string ErrorMessage)> AddToDoForUser(ToDoDbContext context, User user, ToDo toDo)
			{
				if (user != null)
				{
					toDo.Id = Guid.NewGuid();
					toDo.IsCompleted = false;
					toDo.UserId = user.Id;

					context.Add(toDo);
					await context.SaveChangesAsync();

					return (true, toDo, null);
				}
				else
				{
					return (false, null, "User not authenticated");
				}
			}

			public static async Task<(bool Success, ToDo Entity)> UpdateToDo(ToDoDbContext context, Guid id, ToDo updateToDo)
			{
				var toDo = await context.Todos.FindAsync(id);

				if (toDo != null)
				{
					toDo.Title = updateToDo.Title;
					toDo.Detail = updateToDo.Detail;
					toDo.Priority = updateToDo.Priority;
					toDo.IsCompleted = updateToDo.IsCompleted;

					context.Update(toDo);
					await context.SaveChangesAsync();

					return (true, toDo);
				}
				else
				{
					return (false, null);
				}
			}

			public static async Task<(bool Success, ToDo Entity)> DeleteToDo(ToDoDbContext context, Guid id)
			{
				var todo = await context.Todos.FindAsync(id);

				if (todo != null)
				{
					context.Remove(todo);
					await context.SaveChangesAsync();

					return (true, todo);
				}
				else
				{
					return (false, null);
				}
			}
		}
	}
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using ToDoListApi.Models;
using BCrypt.Net;
using ToDoListApi.Helpers;

namespace ToDoListApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		public static User user;
		private readonly ToDoDbContext _toDoDbContext;

		public UserController(ToDoDbContext toDoDbContext)
		{
			_toDoDbContext = toDoDbContext;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> RegisterUser([FromBody] User user)
		{
			var result = await UserHelper.RegisterUser(_toDoDbContext, user);

			if (result.Success)
			{
				return Ok(result.Entity);
			}
			else
			{
				return BadRequest("User registration failed");
			}
		}

		[HttpPost("Login")]
		public async Task<IActionResult> LoginUser([FromBody] User user)
		{
			var result = await UserHelper.LoginUser(_toDoDbContext, user);

			if (result.Success)
			{
				UserController.user = result.Entity;
				return Ok(result.Entity);
			}
			else
			{
				return NotFound("User not found or incorrect password");
			}
		}

		[HttpPost("Logout")]
		public IActionResult LogoutUser()
		{
			UserController.user = null;
			return Ok(new { Message = "Logout successful" });
		}
	}
}

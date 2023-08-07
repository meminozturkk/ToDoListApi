using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using ToDoListApi.Models;
using BCrypt.Net;

namespace ToDoListApi.Helpers
{
	public static class UserHelper
	{
		public static async Task<(bool Success, User Entity)> RegisterUser(ToDoDbContext context, User user)
		{
			if (user == null)
				return (false, null);

			var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);
			if (existingUser != null)
				return (false, null);

			string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
			user.Password = hashedPassword;

			user.Id = Guid.NewGuid();
			context.Users.Add(user);
			await context.SaveChangesAsync();

			return (true, user);
		}

		public static async Task<(bool Success, User Entity)> LoginUser(ToDoDbContext context, User user)
		{
			if (user == null)
				return (false, null);

			var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);
			if (existingUser == null)
				return (false, null);

			bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);

			if (!isPasswordCorrect)
				return (false, null);

			return (true, existingUser);
		}
	}
}
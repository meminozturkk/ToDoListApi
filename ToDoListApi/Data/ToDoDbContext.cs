using Microsoft.EntityFrameworkCore;
using ToDoListApi.Models;

namespace ToDoListApi.Data
{
	public class ToDoDbContext : DbContext
	{
		public ToDoDbContext(DbContextOptions options) : base(options)
		{
		}


		public DbSet<User> Users { get; set; }
		public DbSet<ToDo> Todos { get; set; }

	
	}
}

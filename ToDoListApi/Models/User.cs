using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Models
{
	public class User
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Password { get; set; }

    }
}


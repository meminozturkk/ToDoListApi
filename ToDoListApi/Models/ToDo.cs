using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Models
{
	public class ToDo
	{
		public Guid Id { get; set; }
		[Required]
		public string Title { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public bool IsCompleted { get; set; }
		public string? Detail { get; set; }
		public string Priority { get; set; }

		public Guid UserId { get; set; }
	}

//	public enum PriorityType
//	{
//		Low,
//		Medium,
//		High
//	}
}


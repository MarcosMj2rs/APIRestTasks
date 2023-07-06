using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace APIRestTasks.Model
{
	public class TaskModel
	{
		[Key]
		[Required]
        public int TaskId { get; set; }

		[NotNull]
		public string? Title { get; set; }

		[StringLength(500)]
		public string? Description { get; set; }

		public string? DueDate { get; set; }

        public string? completed { get; set; }

		[Range(1, 5)]
        public short Priority { get; set; }
    }
}

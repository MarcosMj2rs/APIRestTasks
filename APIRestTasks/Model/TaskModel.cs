using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Windows.Markup;

namespace APIRestTasks.Model
{
	[Serializable]
	public class TaskModel
	{
		[Key]
		[Required]
		public int TaskId { get; set; }

		[NotNull]
		public string? Title { get; set; }

		[StringLength(500)]
		public string? Description { get; set; }

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
		public string? DueDate { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");

		public bool completed { get; set; }

		[Range(1, 5, ErrorMessage = "O valor para {0} deve estar entre {1} e {2}.")]
		public short Priority { get; set; }
	}
}

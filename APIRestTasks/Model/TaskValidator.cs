using FluentValidation;
using System.Globalization;

namespace APIRestTasks.Model
{
	public class TaskValidator : AbstractValidator<TaskModel>
	{
		public TaskValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("O campo title não pode estar vazio");

			RuleFor(x => x.Description)
				.Must(x => x != null || x.Length > 500)
				.WithMessage("O campo description deve ter no máximo 500 caracteres");

			RuleFor(x => x.DueDate)
				.Must(ValidarData).WithMessage("O campo dueDate deve ser uma data válida no formato dd-MM-yyyy");

			RuleFor(x => x.completed)
				.Must(IsBoolean).WithMessage("O campo completed deve ser um valor booleano ['true' | 'false']");
		}

		private static bool ValidarData(string data)
		{
			DateTime valor;
			var convertido = DateTime
				.TryParseExact(data,
								"dd-MM-yyyy",
								CultureInfo.InvariantCulture,
								DateTimeStyles.None,
								out valor);
			return valor == DateTime.MinValue ? false : true;
		}

		private static bool IsBoolean(string data)
		{
			var booleanValid = false;
			return bool.TryParse(data, out booleanValid);
		}

		private static bool RangePriority(short data)
		{
			return data < 1 || data > 5 ? false : true;
		}
	}
}

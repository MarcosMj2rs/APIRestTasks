using APIRestTasks.Model;

namespace APIRestTasks.Service
{
	public class TaskService : ITaskService
	{
		private readonly List<TaskModel> tasks;

		public TaskService()
		{
			this.tasks = new List<TaskModel>
			{
				new TaskModel { TaskId = 1, completed = true, Description = "Task 1", DueDate = DateTime.Now.ToString("dd-MM-yyyy"), Priority = 1, Title = "Title Task 1" },
				new TaskModel { TaskId = 2, completed = true, Description = "Task 2", DueDate = DateTime.Now.ToString("dd-MM-yyyy"), Priority = 2, Title = "Title Task 2" },
				new TaskModel { TaskId = 3, completed = false, Description = "Task 3", DueDate = DateTime.Now.ToString("dd-MM-yyyy"), Priority = 3, Title = "Title Task 3" },
				new TaskModel { TaskId = 4, completed = true, Description = "Task 4", DueDate = DateTime.Now.ToString("dd-MM-yyyy"), Priority = 4, Title = "Title Task 4" },
				new TaskModel { TaskId = 5, completed = false, Description = "Task 5", DueDate = DateTime.Now.ToString("dd-MM-yyyy"), Priority = 5, Title = "Title Task 5" }
			};
		}

		public async Task AddTask(TaskModel task)
		{
			//Adiciona task
			this.tasks.Add(task);
			await Task.Delay(1000).ConfigureAwait(true);
		}

		public async Task DeleteTask(int taskId)
		{
			//DeleteTask a task
			var task = GetTask(taskId).Result;
			this.tasks.Remove(task);
			await Task.Delay(1000).ConfigureAwait(true);
		}

		public async Task<List<TaskModel>> GetAllTasks()
		{
			await Task.Delay(1000).ConfigureAwait(false);
			return this.tasks.OrderBy(i => i.Priority).ToList();
		}

		public async Task<TaskModel> GetTask(int taskId)
		{
			await Task.Delay(1000).ConfigureAwait(false);
			return this.tasks.Where(p => p.TaskId == taskId).FirstOrDefault();
		}

		public async Task UpdateTask(int taskId)
		{
			//Atualiza a task
			await Task.Delay(1000).ConfigureAwait(false);
		}
	}
}

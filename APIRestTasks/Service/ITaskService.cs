using APIRestTasks.Model;

namespace APIRestTasks.Service
{
	public interface ITaskService
	{
		Task<List<TaskModel>> GetAllTasks();
		Task<TaskModel> GetTask(int id);
		Task UpdateTask(int id);
		Task AddTask(TaskModel task);
		Task DeleteTask(int id);
	}
}

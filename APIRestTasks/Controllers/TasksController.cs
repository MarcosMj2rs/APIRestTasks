using APIRestTasks.Model;
using APIRestTasks.Service;
using Microsoft.AspNetCore.Mvc;

namespace APIRestTasks.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		private readonly ITaskService _taskService;
		private readonly TaskValidator validationRules;

		public TaskController(ITaskService taskService)
		{
			this._taskService = taskService;
			this.validationRules = new TaskValidator();
		}

		[HttpGet]
		public ActionResult<List<TaskModel>> Get()
		{
			return Ok(this._taskService.GetAllTasks().Result);
		}

		[HttpGet("{id}")]
		public ActionResult<TaskModel> Get(int id)
		{
			try
			{
				return Ok(_taskService.GetTask(id).Result);
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPost]
		public ActionResult<TaskModel> Post([FromBody] TaskModel task)
		{
			try
			{
				var tasks = _taskService.GetAllTasks().Result;
				var resultValidate = this.validationRules.Validate(task);

				if (resultValidate.IsValid)
				{
					this._taskService.AddTask(task);
					return Ok();
				}
				else
				{
					var errorMessages = resultValidate.Errors.Select(x => x.ErrorMessage).ToList();
					return BadRequest(errorMessages);
				}
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPut("{id}")]
		public ActionResult<TaskModel> Put(int id)
		{
			try
			{
				_taskService.UpdateTask(id);
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpDelete("{id}")]
		public ActionResult<TaskModel> Delete(int id)
		{
			try
			{
				_taskService.DeleteTask(id);
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}
	}
}

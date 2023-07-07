using APIRestTasks.Model;
using APIRestTasks.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace APIRestTasks.Controllers
{
	/// <summary>
	/// TaskController
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		#region[[PROPERTIES]]
		private readonly ITaskService _taskService;
		private readonly TaskValidator validationRules;
		JsonSerializerOptions options { get; set; }
		#endregion

		#region[[ACTIONS]]
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="taskService"></param>
		public TaskController(ITaskService taskService)
		{
			this._taskService = taskService;
			this.validationRules = new TaskValidator();

			options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
				WriteIndented = true
			};
		}

		[HttpGet]
		public ActionResult<List<TaskModel>> Get()
		{
			try
			{
				return new JsonResult(this._taskService.GetAllTasks().Result, options);
			}
			catch
			{
				Response.StatusCode = StatusCodes.Status400BadRequest;
				return new JsonResult(Response.StatusCode, options);
			}
		}

		[HttpGet("{id}")]
		public ActionResult<TaskModel> Get(int id)
		{
			try
			{
				return new JsonResult(_taskService.GetTask(id).Result, options);
			}
			catch
			{
				Response.StatusCode = StatusCodes.Status400BadRequest;
				return new JsonResult(Response.StatusCode, options);
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
					return new JsonResult(StatusCodes.Status200OK, options);
				}
				else
				{
					Response.StatusCode = StatusCodes.Status400BadRequest;
					var errorMessages = resultValidate.Errors.Select(x => x.ErrorMessage).ToList();
					return new JsonResult(errorMessages, options);
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
				return new JsonResult(StatusCodes.Status200OK, options);
			}
			catch
			{
				Response.StatusCode = StatusCodes.Status400BadRequest;
				return new JsonResult(Response.StatusCode, options);
			}
		}

		[HttpDelete("{id}")]
		public ActionResult<TaskModel> Delete(int id)
		{
			try
			{
				_taskService.DeleteTask(id);
				return new JsonResult(StatusCodes.Status200OK, options);
			}
			catch
			{
				Response.StatusCode = StatusCodes.Status400BadRequest;
				return new JsonResult(Response.StatusCode, options);
			}
		}
		#endregion
	}
}

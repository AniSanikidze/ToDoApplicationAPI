using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using ToDoApp.API.Infrastructure.SwaggerExamples;
using ToDoApp.Application.ToDos;
using ToDoApp.Application.ToDos.Requests;
using ToDoApp.Application.ToDos.Responses;

namespace ToDo.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _service;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly int userIdClaim;

        public ToDoController(IToDoService todoService, IHttpContextAccessor httpContextAccessor)
        {
            _service = todoService;
            _contextAccessor = httpContextAccessor;
            userIdClaim = GetUserIdClaimFromJWTToken(_contextAccessor);
        }
        /// <summary>
        /// Returns user's todo and corresponding subtasks based on provided id
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <returns>Return specific todo</returns>
        /// <response code="200">Returns the specific todo and corresponding subtasks data</response>
        /// <response code="404">If the todo was not found or todo does not belong to the user</response>
        [ProducesResponseType(typeof(ToDoExample), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ToDoExample))]
        [Produces("application/json")]
        [HttpGet("v1/todos/{id}")]
        public async Task<ActionResult<ToDoResponseModel>> Get(CancellationToken cancellationToken, int id)
        {
            return Ok(await _service.GetAsync(cancellationToken, id, userIdClaim));
        }
        /// <summary>
        /// Returns list of user's todos with subtasks
        /// </summary>
        /// <remarks>
        /// Note: Status is an optional query parameter to filter todos based on the following statuses:
        /// 
        ///     1 - Active
        ///     
        ///     2 - Done
        ///     
        /// By default Status is 0, which retrieves all todos regardless of the status
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <returns>list of todos</returns>
        [ProducesResponseType(typeof(IEnumerable<ToDoResponseModel>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ToDoExamples))]
        [Produces("application/json")]
        [HttpGet("v1/todos/")]
        public async Task<ActionResult<IEnumerable<ToDoResponseModel>>> GetAll(CancellationToken cancellationToken, int status = 0)
        {
            return Ok(await _service.GetAllAsync(cancellationToken,userIdClaim,status));
        }
        /// <summary>
        /// Creates a todo and its corresponding subtasks
        /// </summary>
        /// <remarks>
        /// Note id is not required
        ///
        ///     POST/ToDo - With Subtasks
        ///     
        ///     {
        ///         "title": "ExampleToDo",
        ///         "targetCompletionDate": "2023-02-23T22:33:55.386Z",
        ///         "subtasks": [
        ///             {
        ///               "title": "ExampleSubtask"
        ///             }
        ///          ]    
        ///     }
        ///     POST/ToDo - Without subtasks
        ///     
        ///     {
        ///         "title": "ExampleToDo Without subtasks",
        ///         "targetCompletionDate": "2023-02-23T22:33:55.386Z"  
        ///     }
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="request"></param>
        /// <returns>Returns updated todo with subtasks</returns>
        /// <response code="200">Returns updated todo with subtasks</response>
        [ProducesResponseType(typeof(ToDoExample), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ToDoExample))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpPost("v1/todos/")]
        public async Task<ActionResult<ToDoResponseModel>> Post(CancellationToken cancellationToken, ToDoRequestModel request)
        {
            return Ok(await _service.CreateAsync(cancellationToken, request, userIdClaim));
        }
        /// <summary>
        /// Updates a todo and its corresponding subtasks
        /// </summary>
        /// <remarks>
        /// 
        /// PUT/ToDo - With Subtasks
        ///     
        ///     {
        ///         "title": "Updated ExampleToDo",
        ///         "targetCompletionDate": "2023-02-23T22:33:55.386Z",
        ///         "subtasks": [
        ///             {
        ///               "id": 1,  
        ///               "title": "Updated ExampleSubtask"
        ///             }
        ///          ]    
        ///     }
        ///     
        /// PUT/ToDo - Without subtasks
        ///     
        ///     {
        ///         "title": "Updated ExampleToDo Without subtasks",
        ///         "targetCompletionDate": "2023-02-23T22:33:55.386Z"  
        ///     }
        ///     
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="request"></param>
        /// <returns>Returns newly created todo with subtasks</returns>
        /// <response code="200">Returns newly created todo with subtasks</response>
        /// <response code="404">If the todo or subtask was not found</response>
        [ProducesResponseType(typeof(ToDoExample), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ToDoExample))]
        [Produces("application/json")]
        [HttpPut("v1/todos/{id}")]
        public async Task<ActionResult<ToDoResponseModel>> Put(CancellationToken cancellationToken, ToDoUpdateModel request, int id)
        {
            return Ok(await _service.UpdateAsync(cancellationToken, request, id,userIdClaim));
        }
        /// <summary>
        /// Markes todo status as done
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <returns>Returns todo marked as done</returns>
        /// <response code="200">Returns updated todo with subtasks</response>
        /// <response code="404">If the todo was not found</response>
        [ProducesResponseType(typeof(ToDoExample), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ToDoExample))]
        [Produces("application/json")]
        [HttpPost("v1/todos/{id}/done")]
        public async Task<ActionResult<ToDoResponseModel>> UpdateToDoStatus(CancellationToken cancellationToken, int id)
        {
            return Ok(await _service.UpdateToDoStatusAsync(cancellationToken, id, userIdClaim));
        }

        /// <summary>
        /// Deletes Todo
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <response code="204">If successfully deleted todo</response>
        /// <response code="404">If todo not found</response>        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("v1/todos/{id}")]
        public async Task<ActionResult> Delete(CancellationToken cancellationToken, int id)
        {
            await _service.DeleteAsync(cancellationToken, id, userIdClaim);
            return NoContent();
        }
        /// <summary>
        /// Updates a single property in the specific todo
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <param name="patchRequest"></param>
        /// <returns>Updated todo model</returns>
        [HttpPatch("v1/todos/{id}")]
        public async Task<ActionResult<ToDoResponseModel>> Patch(CancellationToken cancellationToken, int id, [FromBody] JsonPatchDocument<ToDoUpdateModel> patchRequest)
        {
            return Ok(await _service.PatchAsync(cancellationToken, patchRequest, id, userIdClaim));
            
        }
        private int GetUserIdClaimFromJWTToken(IHttpContextAccessor httpContextAccessor)
        {
            var userIdClaim = int.Parse(httpContextAccessor.HttpContext.User.FindFirst("UserId")!.Value);
            return userIdClaim;
        }
    }
}

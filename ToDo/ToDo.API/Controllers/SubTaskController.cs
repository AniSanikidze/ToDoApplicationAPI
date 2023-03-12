using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.ToDos.Requests;
using ToDoApp.Application.ToDos.Responses;
using ToDoApp.Application.ToDos;
using ToDoApp.Application.Subtasks;
using ToDoApp.Application.Subtasks.Responses;
using ToDoApp.Application.Subtasks.Requests;
using Swashbuckle.AspNetCore.Filters;
using ToDoApp.API.Infrastructure.SwaggerExamples;

namespace ToDo.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubtaskController : ControllerBase
    {
        private readonly ISubtaskService _service;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly int userIdClaim;

        public SubtaskController(ISubtaskService todoService, IHttpContextAccessor httpContextAccessor)
        {
            _service = todoService;
            _contextAccessor = httpContextAccessor;
            userIdClaim = GetUserIdClaimFromJWTToken(_contextAccessor);
        }
        /// <summary>
        /// Returns user's subtask based on provided id
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <returns>Return specific todo</returns>
        /// <response code="200">Returns the specific subtasks data</response>
        /// <response code="404">If the subtask was not found or subtask does not belong to the user</response>
        [ProducesResponseType(typeof(SubtaskExample), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SubtaskExample))]
        [Produces("application/json")]
        [HttpGet("v1/subtasks/{id}")]
        public async Task<ActionResult<SubtaskResponseModel>> Get(CancellationToken cancellationToken, int id)
        {
            return Ok(await _service.GetAsync(cancellationToken, id, userIdClaim));
        }
        /// <summary>
        /// Returns list of user's subtasks
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>list of todos</returns>
        /// <response code="200">Returns the specific subtasks data</response>
        /// <response code="404">If the subtask was not found or subtask does not belong to the user</response>
        [ProducesResponseType(typeof(IEnumerable<SubtaskResponseModel>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SubtaskExamples))]
        [Produces("application/json")]
        [HttpGet("v1/subtasks/")]
        public async Task<ActionResult<IEnumerable<SubtaskResponseModel>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAllAsync(cancellationToken, userIdClaim));
        }
        /// <summary>
        /// Returns newly created subtask
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>list of todos</returns>
        [ProducesResponseType(typeof(IEnumerable<SubtaskResponseModel>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SubtaskExamples))]
        [Produces("application/json")]
        [HttpPost("v1/substaks/")]
        public async Task<ActionResult<SubtaskResponseModel>> Post(CancellationToken cancellationToken, SubtaskRequestModel request)
        {
            return Ok(await _service.CreateAsync(cancellationToken, request,userIdClaim));
        }
        /// <summary>
        /// Updates subtask
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns>Updated Subtask</returns>
        [HttpPut("v1/subtasks/{id}")]
        public async Task<ActionResult<SubtaskResponseModel>> Put(CancellationToken cancellationToken, SubtaskRequestModel request, int id)
        {
            return Ok(await _service.UpdateAsync(cancellationToken, request, id, userIdClaim));
        }
        /// <summary>
        /// Deletes subtask
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <returns>No Content</returns>
        [HttpDelete("v1/subtasks/{id}")]
        public async Task<ActionResult> Delete(CancellationToken cancellationToken, int id)
        {
            await _service.DeleteAsync(cancellationToken, id, userIdClaim);
            return NoContent();
        }

        protected int GetUserIdClaimFromJWTToken(IHttpContextAccessor httpContextAccessor)
        {
            var userIdClaim = int.Parse(httpContextAccessor.HttpContext.User.FindFirst("UserId")!.Value);
            return userIdClaim;
        }
    }
}

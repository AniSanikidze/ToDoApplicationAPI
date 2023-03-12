using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ToDoApp.API.Infrastructure.Auth;
using ToDoApp.Application.Users.Responses;
using ToDoApp.Application.Users;
using ToDoApp.Application.Users.Requests;
using Swashbuckle.AspNetCore.Filters;
using ToDoApp.API.Infrastructure.SwaggerExamples;

namespace ToDo.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptions<JWTConfiguration> _options;

        public UserController(IUserService userService, IOptions<JWTConfiguration> options)
        {
            _userService = userService;
            _options = options;
        }
        /// <summary>
        /// registers user with provided username and password
        /// </summary>
        /// <remarks>
        /// Note id is not required
        ///
        ///     POST/User
        ///     
        ///      {
        ///         "username": "ExampleUser",
        ///         "password": "ExamplePassword"
        ///      }   
        /// </remarks>
        /// <param name="cancellation"></param>
        /// <param name="user"></param>
        /// <returns>Returns newly created user data</returns>
        /// <response code="200">Returns new user</response>
        [ProducesResponseType(typeof(UserExample), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserExample))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("/v1/users")]
        [HttpPost]
        public async Task<UserResponseModel> Register(CancellationToken cancellation, UserRequestModel user)
        {
            return await _userService.CreateAsync(cancellation, user);
        }
        /// <summary>
        /// logins user with provided username and password
        /// </summary>
        /// <remarks>
        /// Note id is not required
        ///
        ///     POST/User
        ///     
        ///      {
        ///         "username": "ExampleUser",
        ///         "password": "ExamplePassword"
        ///      }   
        /// </remarks>
        /// <param name="cancellation"></param>
        /// <param name="request"></param>
        /// <returns>Returns jwt token after the user has successfully logged into the system</returns>
        /// <response code="200">Returns jwt token</response>
        /// <response code="404">If user was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("v1/users/access-token")]
        [HttpPost]
        public async Task<string> LogIn(CancellationToken cancellation, UserRequestModel request)
        {
            var result = await _userService.AuthenticateAsync(cancellation, request);

            return JWTHelper.GenerateSecurityToken(result.Username, result.Id, _options);
        }
    }
}

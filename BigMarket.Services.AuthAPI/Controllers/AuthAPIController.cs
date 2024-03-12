using BigMarket.MessageBus;
using BigMarket.Services.AuthAPI.Models.Dto;
using BigMarket.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BigMarket.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController(IAuthService authService,
                                                       IMessageBus messageBus,
                                                        IConfiguration configuration) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IMessageBus _messageBus = messageBus;
        private readonly IConfiguration _configuration = configuration;
        private readonly ResponseDto _response = new();

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            string errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            string topic_queue_Name = _configuration.GetValue<string>("TopikAndQueueNames:RegisterUserQueue");
            await _messageBus.PublishMessageAsync(model.Email, topic_queue_Name);
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            LoginResponseDto loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect"; // TODO change this row from hard code
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered"; // TODO change this row from hard code
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("IsEmailAlreadyRegistered")]
        public async Task<IActionResult> IsEmailAlreadyRegistered([FromBody] string email)
        {
            var isEmailFree = await _authService.IsEmailAlreadyRegistered(email);
            _response.Result = isEmailFree;
            return Ok(_response);
        }

    }
}

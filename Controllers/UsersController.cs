using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackR_API.Models;
using TrackR_API.Models.RequestModel;
using TrackR_API.Models.ResponseModel;
using TrackR_API.Repository.IRepository;

namespace TrackR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("getUserByEmail/{emailAddress}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetUser(string emailAddress)
        {
            try
            {
                var user = await _userRepository.GetUser(emailAddress);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An internal server error occurred. {ex.Message}");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();

                if(users == null)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An internal server error occurred. {ex.Message}");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                await _userRepository.Update(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal server error occurred. {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUser([FromBody] UserRequest user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                User entity = new User
                {
                    CreatedAt = DateTime.Now,
                    Name = user.Name,
                    Email = user.Email,
                    HomeAddress = "Home",
                    HomeLatitude = 0,
                    HomeLongitude = 0,
                    OfficeAddress = "Cascades Office Park, Wasbank St, Little Falls, Roodepoort, 1724",
                    OfficeLatitude = -25.469062531150492,
                    OfficeLongitude = 30.995649256207436,
                    PasswordHash = passwordHash
                };

                await _userRepository.Create(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal server error occurred. {ex.Message}");
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UserLogin([FromBody] UserLoginRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }

                var user = await _userRepository.UserLogin(request);

                if (!user)
                {
                    return Unauthorized();
                }

                Identity identity = new Identity
                {
                    AuthenticationType = "Password Authentication",
                    IsAuthenticated = true,

                };

                return Ok(identity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal server error occurred. {ex.Message}");
            }
        }
    }
}
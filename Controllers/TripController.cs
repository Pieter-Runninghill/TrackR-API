using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackR_API.Models;
using TrackR_API.Repository;
using TrackR_API.Repository.IRepository;

namespace TrackR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;

        public TripController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        [HttpGet("getTripsByUserId/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Trip>>> GetAllTripsByUser(int userId)
        {
            try
            {
                var trips = await _tripRepository.GetTripsByUserId(userId);

                if (trips == null)
                {
                    return NotFound();
                }

                return Ok(trips);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal server error occurred. {ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Trip>>> GetTripById( int Id)
        {
            try
            {
                var trip = await _tripRepository.GetTripById(Id);

                if (trip == null)
                {
                    return NotFound();
                }

                return Ok(trip);
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
        public async Task<ActionResult> Create([FromBody] Trip entity)
        {
            try
            {
                if (entity == null)
                {
                    return BadRequest();
                }

                await _tripRepository.Create(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, $"An internal server error occurred. {ex.Message}");
            }
        }
    }
}

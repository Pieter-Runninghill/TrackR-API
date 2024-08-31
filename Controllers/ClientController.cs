﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackR_API.Models;
using TrackR_API.Repository.IRepository;

namespace TrackR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Client>>> GetAll()
        {
            try
            {
                var clients = await _clientRepository.GetAll();

                if (clients == null)
                {
                    return NotFound();
                }

                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal server error occurred. {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Client>>> GetClientById(int id)
        {
            try
            {
                var client = await _clientRepository.GetById(id);

                if (client == null)
                {
                    return NotFound();
                }

                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal server error occurred. {ex.Message}");
            }
        }
    }
}

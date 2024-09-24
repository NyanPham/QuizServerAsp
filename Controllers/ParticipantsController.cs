using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApi.DTOs;
using QuizApi.Repositories;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly ParticipantRepository _participantRepository;

        public ParticipantsController(ParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        // GET: api/Participants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipantToQueryDTO>>> GetParticipants()
        {
            var participants = await _participantRepository.GetAllAsync();
            return Ok(participants);
        }

        // GET: api/Participants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipantToQueryDTO>> GetParticipant(int id)
        {
            try
            {
                var participant = await _participantRepository.GetByIdAsync(id);
                return Ok(participant);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/Participants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant(int id, ParticipantToUpdateDTO participantToUpdateDTO)
        {
            try
            {
                await _participantRepository.UpdateAsync(id, participantToUpdateDTO);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Participants
        [HttpPost]
        public async Task<ActionResult<ParticipantToQueryDTO>> PostParticipant(ParticipantToCreateDTO participantToCreateDTO)
        {
            var participant = await _participantRepository.CreateAsync(participantToCreateDTO);
            return CreatedAtAction(nameof(GetParticipant), new { id = participant.Id }, participant);
        }

        // DELETE: api/Participants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            try
            {
                await _participantRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
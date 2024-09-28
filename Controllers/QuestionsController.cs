using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApi.DTOs;
using QuizApi.Models;
using QuizApi.Repositories;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionRepository _questionRepository;

        public QuestionsController(QuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionToQueryDTO>>> GetQuestions()
        {
            var random5Questions = (await _questionRepository.GetAllAsync())
                                    .OrderBy(y => Guid.NewGuid())
                                    .Take(5)
                                    .ToList();
            return Ok(random5Questions);
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionToQueryDTO>> GetQuestion(int id)
        {
            try
            {
                var question = await _questionRepository.GetByIdAsync(id);
                return Ok(question);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, QuestionToUpdateDTO question)
        {
            try
            {
                await _questionRepository.UpdateAsync(id, question);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Questions
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<QuestionToQueryDTO>> PostQuestion([FromForm] QuestionToCreateDTO question)
        {
            // Console.WriteLine(Request.Headers["Authorization"]);

            if (question.image != null && question.image.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + question.image.FileName;
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await question.image.CopyToAsync(stream);
                }

                question.ImageName = uniqueFileName;
            }

            question.OptionsArr = question.Options.Split("**OPTION_DELIMITER**");

            var createdQuestion = await _questionRepository.CreateAsync(question);
            return CreatedAtAction("GetQuestion", new { id = createdQuestion.Id }, createdQuestion);
        }

        // POST: api/Questions/Answers
        [HttpPost]
        [Route("Answers")]
        public async Task<ActionResult<IEnumerable<QuestionToQueryDTO>>> RetrieveAnswers(int[] questionIds)
        {
            var answers = (await _questionRepository.GetAllAsync())
                            .Where(x => questionIds.Contains(x.Id))
                            .ToList();

            return Ok(answers);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                await _questionRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", image.FileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return Ok(new { ImageName = image.FileName });
        }
    }
}
using Microsoft.EntityFrameworkCore;
using QuizApi.Data;
using QuizApi.DTOs;
using QuizApi.Models;

namespace QuizApi.Repositories
{
    public class QuestionRepository : IRepository<Question, QuestionToQueryDTO, QuestionToCreateDTO, QuestionToUpdateDTO>
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuestionToQueryDTO>> GetAllAsync()
        {
            var questions = await _context.Questions.ToListAsync();

            return questions.Select(q => new QuestionToQueryDTO
            {
                // map properties from Question to QuestionToQueryDTO
                Id = q.Id,
                QuestionInWords = q.QuestionInWords,
                Answer = q.Answer,
                Options = q.Options,
                ImageName = q.ImageName
            });
        }

        public async Task<QuestionToQueryDTO> CreateAsync(QuestionToCreateDTO entity)
        {
            var question = new Question
            {
                QuestionInWords = entity.QuestionInWords,
                Answer = entity.Answer,
                Options = entity.OptionsArr,
                ImageName = entity.ImageName
            };

            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            return new QuestionToQueryDTO
            {
                Id = question.Id,
                QuestionInWords = question.QuestionInWords,
                Answer = question.Answer,
                Options = question.Options,
                ImageName = question.ImageName
            };
        }

        public async Task<QuestionToQueryDTO> GetByIdAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                throw new KeyNotFoundException();
            }

            return new QuestionToQueryDTO
            {
                Id = question.Id,
                QuestionInWords = question.QuestionInWords,
                Answer = question.Answer,
                Options = question.Options,
                ImageName = question.ImageName
            };
        }

        public async Task UpdateAsync(int id, QuestionToUpdateDTO entity)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                throw new KeyNotFoundException();
            }

            question.QuestionInWords = entity.QuestionInWords;
            question.Answer = entity.Answer;
            question.Options = entity.Options;
            question.ImageName = entity.ImageName;

            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}
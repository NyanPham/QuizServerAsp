using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using QuizApi.Data;
using QuizApi.Models;

namespace QuizApi.Repositories
{
    public class ParticipantRepository : IRepository<Participant>
    {
        private readonly ApplicationDbContext _context;

        public ParticipantRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Participant>> GetAllAsync()
        {
            return await _context.Participants.ToListAsync();
        }

        public async Task<Participant> CreateAsync(Participant entity)
        {
            await _context.Participants.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Participant> GetByIdAsync(int id)
        {
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                throw new KeyNotFoundException();
            }

            return participant;
        }

        public async Task UpdateAsync(int id, Participant entity)
        {
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                throw new KeyNotFoundException();
            }

            participant.Score = entity.Score;
            participant.TimeTaken = entity.TimeTaken;

            _context.Entry(participant).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Remove(participant);
            await _context.SaveChangesAsync();
        }
    }
}
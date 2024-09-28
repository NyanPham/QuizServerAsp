using Microsoft.EntityFrameworkCore;
using QuizApi.Data;
using QuizApi.DTOs;
using QuizApi.Models;

namespace QuizApi.Repositories
{
    public class ParticipantRepository : IRepository<Participant, ParticipantToQueryDTO, ParticipantToCreateDTO, ParticipantToUpdateDTO>
    {
        private readonly ApplicationDbContext _context;

        public ParticipantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParticipantToQueryDTO>> GetAllAsync()
        {
            var participants = await _context.Participants.ToListAsync();

            return participants.Select(p => new ParticipantToQueryDTO
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Score = p.Score,
                TimeTaken = p.TimeTaken,
                User = p.User
            });
        }

        public async Task<ParticipantToQueryDTO> CreateAsync(ParticipantToCreateDTO entity)
        {
            var participant = new Participant
            {
                Name = entity.Name,
                Email = entity.Email,
                Score = entity.Score,
                TimeTaken = entity.TimeTaken,
                UserId = entity.UserId
            };

            await _context.Participants.AddAsync(participant);
            await _context.SaveChangesAsync();

            return new ParticipantToQueryDTO
            {
                Id = participant.Id,
                Name = participant.Name,
                Email = participant.Email,
                Score = participant.Score,
                TimeTaken = participant.TimeTaken,
                User = participant.User
            };
        }

        public async Task<ParticipantToQueryDTO> GetByUserIdAsync(string userId)
        {
            var participant = await _context.Participants.FirstOrDefaultAsync(p => p.UserId == userId);

            if (participant == null)
            {
                throw new KeyNotFoundException();
            }

            return new ParticipantToQueryDTO
            {
                Id = participant.Id,
                Name = participant.Name,
                Email = participant.Email,
                Score = participant.Score,
                TimeTaken = participant.TimeTaken,
                User = participant.User
            };
        }

        public async Task<ParticipantToQueryDTO> GetByIdAsync(int id)
        {
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                throw new KeyNotFoundException();
            }

            return new ParticipantToQueryDTO
            {
                Id = participant.Id,
                Name = participant.Name,
                Email = participant.Email,
                Score = participant.Score,
                TimeTaken = participant.TimeTaken,
                User = participant.User
            };
        }

        public async Task UpdateAsync(int id, ParticipantToUpdateDTO entity)
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
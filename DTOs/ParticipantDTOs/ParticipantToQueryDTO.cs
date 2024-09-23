using Microsoft.AspNetCore.Identity;

namespace QuizApi.DTOs
{
    public class ParticipantToQueryDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int TimeTaken { get; set; }
        public IdentityUser? User { get; set; }
    }
}
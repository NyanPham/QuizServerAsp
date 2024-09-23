namespace QuizApi.DTOs
{
    public class ParticipantToCreateDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int TimeTaken { get; set; }
        public string? UserId { get; set; }
    }
}
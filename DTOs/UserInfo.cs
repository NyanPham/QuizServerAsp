namespace QuizApi.DTOs
{
    public class UserInfo
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
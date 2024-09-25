using System.ComponentModel.DataAnnotations;

namespace QuizApi.DTOs
{
    public class QuestionToCreateDTO
    {
        [Required]
        [StringLength(250)]
        public string QuestionInWords { get; set; } = string.Empty;

        [StringLength(250)]
        public string? ImageName { get; set; }
        public IFormFile? image { get; set; }

        [Required]
        public string Options { get; set; } = string.Empty;

        public string[] OptionsArr { get; set; } = Array.Empty<string>();

        [Required]
        public int Answer { get; set; }
    }
}

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

        [Required]
        public string[] Options { get; set; } = Array.Empty<string>();
        
        [Required]
        public int Answer { get; set; }
    }
}

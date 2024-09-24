using System.ComponentModel.DataAnnotations;

namespace QuizApi.DTOs
{
    public class QuestionToUpdateDTO
    {
        [Required]
        [StringLength(250)]
        public string QuestionInWords { get; set; } = string.Empty;

        [StringLength(250)]
        public string? ImageName { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 1)]
        public string[] Options { get; set; } = Array.Empty<string>();
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Answer must be a valid option")]
        public int Answer { get; set; }
    }
}

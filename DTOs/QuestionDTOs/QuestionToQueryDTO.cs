using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    public class QuestionToQueryDTO
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string QuestionInWords { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(250)")]
        public string? ImageName { get; set; }

        [Column(TypeName = "nvarchar(250)[]")]
        public string[] Options { get; set; } = Array.Empty<string>();
        
        public int Answer { get; set; }
    }
}


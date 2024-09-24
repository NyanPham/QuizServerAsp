using QuizApi.Models;

namespace QuizApi.Data
{
    public class SeedQuestions
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            if (!context.Questions.Any())
            {
                await context.Questions.AddRangeAsync(
                    new Question { QuestionInWords = "What does HTML stand for?", Options = new[] { "Hyper Trainer Marking Language", "Hyper Text Marketing Language", "Hyper Text Markup Language", "Hyper Text Markup Leveler" }, Answer = 2 },
                    new Question { QuestionInWords = "The brain of any computer system is", Options = new[] { "ALU", "Memory", "CPU", "Control unit" }, Answer = 2 },
                    new Question { QuestionInWords = "Which of the following computer language is used for artificial intelligence?", Options = new[] { "FORTRAN", "PROLOG", "C", "COBOL" }, Answer = 1 },
                    new Question { QuestionInWords = "What is the primary requisite of a good computer programmer?", Options = new[] { "Mathematical Mind", "Artistic mind", "Logical Mind", "Scientific Knowledge" }, Answer = 2 },
                    new Question { QuestionInWords = "Name the device.", ImageName = "mouse.png", Options = new[] { "Keyboard", "Monitor", "Mouse", "Graphics Card" }, Answer = 2 },
                    new Question { QuestionInWords = "The first mechanical computer designed by Charles Babbage was called ?", Options = new[] { "Analytical Engine", "Calculator", "Processor", "Abacus" }, Answer = 0 },
                    new Question { QuestionInWords = "One byte is equivalent to ?", Options = new[] { "4 bits", "8 bits", "16 bits", "32 bits" }, Answer = 1 },
                    new Question { QuestionInWords = "Web pages are written using ?", Options = new[] { "FTP", "UML", "HTML", "URL" }, Answer = 2 },
                    new Question { QuestionInWords = "Which of the following is NOT operating system ?", Options = new[] { "Dos", "Unix", "Window NT", "CSS" }, Answer = 3 },
                    new Question { QuestionInWords = "What is the full form of lP ?", Options = new[] { "Interface Program", "Interface Protocol", "Internet program", "Internet Protocol" }, Answer = 3 }                );
                await context.SaveChangesAsync();
            }
        }
    }
}

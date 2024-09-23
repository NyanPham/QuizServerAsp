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
                    new Question { QuestionInWords = "What does HTML stand for?", Option1 = "Hyper Trainer Marking Language", Option2 = "Hyper Text Marketing Language", Option3 = "Hyper Text Markup Language", Option4 = "Hyper Text Markup Leveler", Answer = 2 },
                    new Question { QuestionInWords = "The brain of any computer system is", Option1 = "ALU", Option2 = "Memory", Option3 = "CPU", Option4 = "Control unit", Answer = 2 },
                    new Question { QuestionInWords = "Which of the following computer language is used for artificial intelligence?", Option1 = "FORTRAN", Option2 = "PROLOG", Option3 = "C", Option4 = "COBOL", Answer = 1 },
                    new Question { QuestionInWords = "What is the primary requisite of a good computer programmer?", Option1 = "Mathematical Mind", Option2 = "Artistic mind", Option3 = "Logical Mind", Option4 = "Scientific Knowledge", Answer = 2 },
                    new Question { QuestionInWords = "Name the device.", ImageName = "mouse.png", Option1 = "Keyboard", Option2 = "Monitor", Option3 = "Mouse", Option4 = "Graphics Card", Answer = 2 },
                    new Question { QuestionInWords = "The first mechanical computer designed by Charles Babbage was called ?", Option1 = "Analytical Engine", Option2 = "Calculator", Option3 = "Processor", Option4 = "Abacus", Answer = 0 },
                    new Question { QuestionInWords = "One byte is equivalent to ?", Option1 = "4 bits", Option2 = "8 bits", Option3 = "16 bits", Option4 = "32 bits", Answer = 1 },
                    new Question { QuestionInWords = "Web pages are written using ?", Option1 = "FTP", Option2 = "UML", Option3 = "HTML", Option4 = "URL", Answer = 2 },
                    new Question { QuestionInWords = "Which of the following is NOT operating system ?", Option1 = "Dos", Option2 = "Unix", Option3 = "Window NT", Option4 = "CSS", Answer = 3 },
                    new Question { QuestionInWords = "What is the full form of lP ?", Option1 = "Interface Program", Option2 = "Interface Protocol", Option3 = "Internet program", Option4 = "Internet Protocol", Answer = 3 }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
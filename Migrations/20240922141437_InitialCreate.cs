using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    TimeTaken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionInWords = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Option1 = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Option2 = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Option3 = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Option4 = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Answer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "ImageName", "Option1", "Option2", "Option3", "Option4", "QuestionInWords" },
                values: new object[,]
                {
                    { 1, 2, null, "Hyper Trainer Marking Language", "Hyper Text Marketing Language", "Hyper Text Markup Language", "Hyper Text Markup Leveler", "What does HTML stand for?" },
                    { 2, 2, null, "ALU", "Memory", "CPU", "Control unit", "The brain of any computer system is" },
                    { 3, 1, null, "FORTRAN", "PROLOG", "C", "COBOL", "Which of the following computer language is used for artificial intelligence?" },
                    { 4, 2, null, "Mathematical Mind", "Artistic mind", "Logical Mind", "Scientific Knowledge", "What is the primary requisite of a good computer programmer?" },
                    { 5, 2, "mouse.png", "Keyboard", "Monitor", "Mouse", "Graphics Card", "Name the device." },
                    { 6, 0, null, "Analytical Engine", "Calculator", "Processor", "Abacus", "The first mechanical computer designed by Charles Babbage was called ?" },
                    { 7, 1, null, "4 bits", "8 bits", "16 bits", "32 bits", "One byte is equivalent to ?" },
                    { 8, 2, null, "FTP", "UML", "HTML", "URL", "Web pages are written using ?" },
                    { 9, 3, null, "Dos", "Unix", "Window NT", "CSS", "Which of the following is NOT operating system ?" },
                    { 10, 3, null, "Interface Program", "Interface Protocol", "Internet program", "Internet Protocol", "What is the full form of lP ?" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}

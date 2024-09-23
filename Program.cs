using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using QuizApi.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Images")
    ),
    RequestPath = "/Images"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await SeedQuestions.Seed(context);

    var seedRoles = new SeedRoles(scope.ServiceProvider);

    await seedRoles.SeedAdmin();
    await seedRoles.SeedParticipant();

    // Create a default admin user
    var adminEmail = "admin@example.com";
    var adminPassword = "Admin@123";
    var participantEmail = "participant@example.com";
    var participantPassword = "Participant@123";

    var seedUsers = new SeedUsers(scope.ServiceProvider);
    await seedUsers.Seed(adminEmail, adminPassword, Roles.Admin);
    await seedUsers.Seed(participantEmail, participantPassword, Roles.Participant);
}

app.Run();


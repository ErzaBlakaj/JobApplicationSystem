using crypto;
using Microsoft.EntityFrameworkCore;
using ResumeModuleApp.Controllers;
using ResumeModuleApp.DataService;
using Org.BouncyCastle.Security;
using iText.Kernel.Pdf;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // i tregon aplikacionit qe ka automapper
builder.Services.AddControllers();




//TODO Get con string from appsettings json

var connectionString = builder.Configuration.GetSection("ConnectionString").Value;
builder.Services.AddDbContext<ResumeContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IResumePdfService, ResumePDFfile>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

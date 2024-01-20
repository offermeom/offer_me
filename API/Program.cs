using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// ** Configure CORS Policy & Configure OMContext with SQL Server & Register UserService Implementation
builder.Services.AddCors(options => options.AddDefaultPolicy(config => config.WithOrigins("http://localhost:port").AllowAnyHeader().AllowAnyMethod())).AddDbContext<OMContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))).AddScoped<IUserService, UserService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseSwagger().UseSwaggerUI();
app.UseCors().UseHttpsRedirection().UseAuthorization();
app.MapControllers();
app.Run();
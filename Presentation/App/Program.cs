using AdministratioSchool.Infraestructure.Persistence.Contex;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using AdministratioSchool.Application.Mappers;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Connection");

//register the database context

builder.Services.AddDbContext<AppDbContext>(Options => 
Options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 28))
));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add the mappers

// Make the mapper configuration, adding the profiles userMapper 
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserMapper());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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

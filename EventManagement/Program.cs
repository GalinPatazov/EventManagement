using EventManagement.InfraStructure;
using EventManagement.InfraStructure.Mapper;
using EventManagement.Services.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<EventValidator>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

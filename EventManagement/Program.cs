using EventManagement.InfraStructure;
using EventManagement.Services.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();


app.UseHttpsRedirection();

app.MapControllers();

app.Run();

builder.Services.AddValidatorsFromAssemblyContaining<EventValidator>();
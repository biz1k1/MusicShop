using MusicShop.Presentation.Common.FilterError;
using FluentValidation;
using MusicShop.Application.Common.Behavior;
using MusicShop.Presentation.Common.DTOs.Authentication;
using MusicShop.Application;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  
app.UseAuthorization();

app.MapControllers();
app.Run();

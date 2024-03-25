using MusicShop.Application;
using Microsoft.AspNetCore.CookiePolicy;




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
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy=SameSiteMode.Strict,
    HttpOnly=HttpOnlyPolicy.Always,
    Secure=CookieSecurePolicy.Always
});
app.MapControllers();
app.Run();

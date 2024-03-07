using Microsoft.EntityFrameworkCore;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Application.Services.FullTreeCategories;
using MusicShop.Infrastructure.Data;
using MusicShop.Presentation.Common.FilterError;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));
builder.Services.AddScoped<ICategoryServicesHandler,CategoryServicesHandler>();
builder.Services.AddScoped<IFullTreeCategoryService,FullTreeCategoriesService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers(options=>options.Filters.Add<GlobalErrorHandlingFilter>());
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

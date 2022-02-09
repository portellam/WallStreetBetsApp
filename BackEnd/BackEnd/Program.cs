using BackEnd.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<WallStreetBetsContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // 45:50 or so is where I left off. Added the Microsoft.EntityFrameworkCore at the top.
});

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "LocalOriginsPolicy",
			builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
			  );
}
	);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("LocalOriginsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


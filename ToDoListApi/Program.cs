using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;

namespace ToDoListApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDbContext<ToDoDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoListContext"));
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			});
			// Add services to the container.

			builder.Services.AddControllers();
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

			app.UseHttpsRedirection();

			app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}

using Microsoft.EntityFrameworkCore;
using TaxiDispatch.API.Data;

namespace TaxiDispatch.API.ServiceComponents
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Добавьте конфигурацию контекста базы данных
            builder.Services.AddDbContext<DispatchtaxiContext>(options =>
                options.UseMySQL("Server=localhost;Port=3306;Database=dispatchtaxi;Uid=root;Pwd=qwerty123;"));

            var optionsBuilder = new DbContextOptionsBuilder<DispatchtaxiContext>();
            optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=dispatchtaxi;Uid=root;Pwd=qwerty123;");

            using (var context = new DispatchtaxiContext(optionsBuilder.Options))
            {
                try
                {
                    context.Database.OpenConnection();
                    Console.WriteLine("Подключение к базе данных MySQL успешно установлено!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка подключения к базе данных MySQL: {ex.Message}");
                }
            }

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
        }
    }
}
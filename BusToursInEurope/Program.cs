using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Services;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.Extensions.Configuration;

namespace BusToursInEurope
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IShowTours, ShowToursService>();
            builder.Services.AddDbContext<ApplicationContext>();

            using (ApplicationContext db = new ApplicationContext())
            {
                Tour tour1 = new Tour { Name = "Italy", Price = 333, StartDate = new DateTime(2025, 07, 12), EndDate = new DateTime(2025, 07, 22), Route = "Rome -> Florence -> Venice", NumOfSeats = 40, Description = "qwe" };
                Tour tour2 = new Tour { Name = "France", Price = 400, StartDate = new DateTime(2025, 08, 10), EndDate = new DateTime(2025, 08, 25), Route = "Rome -> Florence -> Venice", NumOfSeats = 30, Description = "qwe" };

                db.Tours.Add(tour1);
                db.Tours.Add(tour2);
                //db.SaveChanges();
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Services;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;

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

            builder.Services.AddScoped<ITours, ToursService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddDbContext<ApplicationContext>();

            using (ApplicationContext db = new ApplicationContext())
            {
                var city = new City { Name = "Minsk", Country = "Belarus", Visa = false };
                db.Cities.Add(city);
                //db.SaveChanges();

                var user = new User { Email = "user@example.com", FullName = "John Doe", UserName = "johndoe", Login = "john_login", Password = "password123", NumPhone = "+123456789", IsAdmin = true, IsUser = false};
                db.Users.Add(user);
                //db.SaveChanges();

                var hotel = new Hotel { Name = "Hotel Minsk", Rating = 4.5, CityId = city.Id };
                db.Hotels.Add(hotel);
                //db.SaveChanges();

                var bus = new Bus { Name = "City Bus", NumOfSeats = 50 };
                db.Buses.Add(bus);
                //db.SaveChanges();

                var route = new RouteBus { Distance = 120.5f, BorderPlace = "Border Crossing" };
                db.Routes.Add(route);
                //db.SaveChanges();

                var tour = new Tour { Name = "City Tour", Price = 299.99m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), NumOfSeats = 50, Description = "A wonderful city tour", BusId = bus.Id, RouteBusId = route.Id };
                db.Tours.Add(tour);
                //db.SaveChanges();

                var reservation = new Reservation { Date = DateTime.Now, PaymentDate = DateTime.Now, PaymentDeadline = DateTime.Now.AddDays(1), NumOfSeats = 2, UserId = user.Id, TourId = tour.Id };
                db.Reservations.Add(reservation);
                //db.SaveChanges();

                var review = new Review { FullName = "John Doe", Rating = 5.0, Comment = "Amazing tour!", ReviewDate = DateTime.Now, UserId = user.Id, TourId = tour.Id };
                db.Reviews.Add(review);
                //db.SaveChanges();

                var waypoint = new WayPoint { NamePlace = "Central Park", CityId = city.Id, RouteBusId = route.Id, HotelId = hotel.Id };
                db.WayPoints.Add(waypoint);
                //db.SaveChanges();



                Tour tour1 = new Tour { Name = "Italy", Price = 333, StartDate = new DateTime(2025, 07, 12), EndDate = new DateTime(2025, 07, 22), NumOfSeats = 40, Description = "qwe", BusId = bus.Id, RouteBusId = route.Id };
                Tour tour2 = new Tour { Name = "France", Price = 400, StartDate = new DateTime(2025, 08, 10), EndDate = new DateTime(2025, 08, 25),  NumOfSeats = 30, Description = "qwe", BusId = bus.Id, RouteBusId = route.Id };

                db.Tours.Add(tour1);
                db.Tours.Add(tour2);

                //db.SaveChanges();
            }

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}

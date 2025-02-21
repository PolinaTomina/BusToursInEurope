using BusToursInEurope.Application.Models.DbModel;
using BusToursInEurope.Application.Models.ReservationModel;
using BusToursInEurope.Application.Models.ReviewModels;
using BusToursInEurope.Application.Models.RoutesBusModels;
using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Models.TourModel
{
    public class FullTourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfSeats { get; set; }
        public string Description { get; set; }

        public BusDto BusDto { get; set; }

        public RouteBusDto RouteBusDto { get; set; }

        public ICollection<ReservationDto> ReservationsDto { get; set; } = new List<ReservationDto>();
        public ICollection<ReviewDto> ReviewsDto { get; set; } = new List<ReviewDto>();
    }
}

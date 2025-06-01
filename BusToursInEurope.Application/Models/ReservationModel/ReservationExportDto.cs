using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Application.Models.UserModel;

namespace BusToursInEurope.Application.Models.ReservationModel
{
    public class ReservationExportDto
    {
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime PaymentDeadline { get; set; }
        public int NumOfSeats { get; set; }
        public UserDto UserEmail { get; set; }
        public ExportExcelReservationTourDto ExportExcelReservationTour { get; set; }
    }
}

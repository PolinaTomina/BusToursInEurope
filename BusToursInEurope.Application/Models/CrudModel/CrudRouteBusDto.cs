using BusToursInEurope.Application.Models.DbModel;

namespace BusToursInEurope.Application.Models.CrudModel
{
    public class CrudRouteBusDto
    {
        public int Id { get; set; }
        public float Distance { get; set; }

        public int WayPointDto { get; set; }
    }
}

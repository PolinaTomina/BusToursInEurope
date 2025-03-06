namespace BusToursInEurope.Application.Models.CityModel
{
    public class CityFilter
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public bool? VisaRequired { get; set; }

        public string? SortBy { get; set; } // Поле для сортировки ("Name", "Country")
        public bool IsDescending { get; set; } = false; // Флаг сортировки (true – по убыванию)
    }
}

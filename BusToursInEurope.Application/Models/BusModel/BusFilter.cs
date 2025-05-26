namespace BusToursInEurope.Application.Models.BusModel
{
    public class BusFilter
    {
        public string? Name { get; set; }
        public int? MinSeats { get; set; }
        public int? MaxSeats { get; set; }

        public string? SortBy { get; set; } // Поле для сортировки (например, "Name", "NumOfSeats")
        public bool IsDescending { get; set; } = false; // Флаг сортировки (true – по убыванию)
    }
}

namespace BusToursInEurope.Application.Models.ProfileModels
{
    public record UpdateProfileDto
    {
        public string? Name { get; set; }

        public string? MiddleName { get; set; }

        public string? SurName { get; set; }

        public string? NumPhone { get; set; }

        public string? PassportNumber { get; set; }
    }
}

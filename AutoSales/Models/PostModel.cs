namespace AutoSales.Models
{
    public class PostModel
    {
        public Guid IdPost { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string VehicleType { get; set; } = null!;
        public string? Edition { get; set; }
        public double Price { get; set; }
        public int FirstRegistration { get; set; }
        public int Mileage { get; set; }
        public int Power { get; set; }
        public string FuelType { get; set; } = null!;
        public string EngineCapacity { get; set; } = null!;
        public string NumberOfDoors { get; set; } = null!;
        public string Colour { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string IdUser { get; set; }
        public string Location { get; set; } = null!;
    }
}

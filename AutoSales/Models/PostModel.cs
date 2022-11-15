using System.ComponentModel.DataAnnotations;

namespace AutoSales.Models
{
    public class PostModel
    {
        public Guid IdPost { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string VehicleType { get; set; } = null!;
        public string? Edition { get; set; }
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime FirstRegistration { get; set; }
        public int Mileage { get; set; }
        public int Power { get; set; }
        public string FuelType { get; set; } = null!;
        public int EngineCapacity { get; set; } 
        public int NumberOfDoors { get; set; } 
        public string Colour { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid IdUser { get; set; }
        public string Image { get; set; } = null!;

        public string Location { get; set; } = null!;
    }
}

using Microsoft.Extensions.Primitives;

namespace AutoSales.Models
{
    public class UserModel
    {
        public Guid IdUser { get; set; }
        public string Name { get; set; } = null!;
        public string YearOfBirth { get; set; } = null!;
        public string? NumberOfPosts { get; set; }
        public string FirstRegistered { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
    }
}

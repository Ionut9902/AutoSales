using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace AutoSales.Models
{
    public class UserModel
    {
        public Guid IdUser { get; set; }
        public string Name { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public string YearOfBirth { get; set; } = null!;
        public int? NumberOfPosts { get; set; }
        public DateTime FirstRegistered { get; set; }
        public string EmailAddress { get; set; } = null!;
    }
}

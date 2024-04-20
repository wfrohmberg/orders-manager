using OrdersManager.Models;
using System.ComponentModel.DataAnnotations;

namespace OrdersManager.Controllers.Models.Addresses
{
    public class AddressWithId
    {
        public int Id { get; set; }
        [RegularExpression("^\\d{2}-\\d{3}$")]
        public string PostalCode { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        public string? FlatNumber { get; set; }
        [Required]
        public string City { get; set; }
        [RegularExpression("^(?<!\\w)(\\(?(\\+|00)?48\\)?)?[ -]?\\d{3}[ -]?\\d{3}[ -]?\\d{3}(?!\\w)$")]
        public string? PhoneNumber { get; set; }
        [RegularExpression("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$")]
        public string? EmailAddress { get; set; }

        public AddressWithId(Address address)
        {
            Id = address.Id;
            PostalCode = address.PostalCode;
            Street = address.Street;
            HouseNumber = address.HouseNumber;
            FlatNumber = address.FlatNumber;
            City = address.City;
            PhoneNumber = address.PhoneNumber;
            EmailAddress = address.EmailAddress;
        }
    }
}

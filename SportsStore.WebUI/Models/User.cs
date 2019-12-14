using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Models
{
    public class User
    {
        public string Name { get; set; }

        public string Surname{ get; set; }

        public string Adress { get; set; }

        public string City { get; set; }

        public string KodPocztowy { get; set; }

        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
        public string Telefon { get; set; }

        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
        public string Email { get; set; }
    }
}
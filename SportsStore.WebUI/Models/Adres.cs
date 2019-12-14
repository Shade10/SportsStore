using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    public class Adres
    {
        [Required(ErrorMessage = "nie podnao miasta")]
        public string City { get; set; }
        [Required(ErrorMessage = "nie podano ulicy")]
        public string Stret { get; set; }
        [Required(ErrorMessage = "nie podano kodu pocztowego")]
        public string Code { get; set; }
    }
}
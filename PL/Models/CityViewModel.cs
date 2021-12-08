using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class CityViewModel : BaseViewModel
    {
        [Display(Name = "Адресс")]
        public string Name { get; set; }
    }
}

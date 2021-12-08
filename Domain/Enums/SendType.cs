using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum SendType
    {
        [Display(Name = "Письмо")]
        Letter,
        [Display(Name = "Габаритный груз")]
        OverallCargo,
        [Display(Name = "Негабаритный груз")]
        OversizedCargo
    }
}

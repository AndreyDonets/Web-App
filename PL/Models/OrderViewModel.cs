using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class OrderViewModel : BaseViewModel
    {
        public OrderViewModel() => Id = Guid.NewGuid();

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Вес кг")]
        public double Weight { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        [Display(Name = "Стоимость")]
        public decimal Price { get; set; }

        [Display(Name = "Адресс")]
        public Guid CityId { get; set; }

        [Display(Name = "Адресс")]
        public CityViewModel City { get; set; }

        [Display(Name = "Тип отправки")]
        public SendType SendType { get; set; }
    }
}

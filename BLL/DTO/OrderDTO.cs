using Domain.Enums;
using System;

namespace BLL.DTO
{
    public class OrderDTO : BaseModelDTO
    {
        public OrderDTO() => Id = Guid.NewGuid();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Weight { get; set; }
        public string Comment { get; set; }
        public decimal Price { get; set; }
        public Guid CityId { get; set; }
        public CityDTO City { get; set; }
        public SendType SendType { get; set; }
    }
}

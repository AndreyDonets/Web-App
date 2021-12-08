using Domain.Enums;
using System;

namespace DAL.Entities
{
    public class Order : BaseEntity
    {
        public Order() => Id = Guid.NewGuid();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Weight { get; set; }
        public string Comment { get; set; }
        public decimal Price { get; set; }
        public Guid CityId { get; set; }
        public virtual City City { get; set; }
        public SendType SendType { get; set; }
    }
}

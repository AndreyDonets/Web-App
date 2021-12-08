using System;

namespace DAL.Entities
{
    public class City : BaseEntity
    {
        public City() => Id = Guid.NewGuid();

        public string Name { get; set; }
    }
}

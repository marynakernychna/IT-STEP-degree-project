using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Ware
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string PhotoLink { get; set; }
        public int AvailableCount { get; set; }
        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Characteristic> Characteristics { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Rate> Rates { get; set; }
    }
}

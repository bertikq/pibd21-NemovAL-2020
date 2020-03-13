using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AbstractTravelCompanyDatabaseImplement.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string TourName { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("ComponentId")]
        public virtual List<TourComponent> TourComponents { get; set; }

        [ForeignKey("TourId")]
        public virtual List<Order> Orders { get; set; }
    }
}

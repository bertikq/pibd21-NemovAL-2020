using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyFileImplement.Models
{
    public class TourComponent
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int ComponentId { get; set; }
        public int Count { get; set; }
    }
}

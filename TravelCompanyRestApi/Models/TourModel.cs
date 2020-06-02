using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelCompanyRestApi.Models
{
    public class TourModel
    {
        public int Id { get; set; }
        public string TourName { get; set; }
        public decimal Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyListImplement.Models
{
    public class StoreComponent
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ComponentId { get; set; }
        public int Count { get; set; }
    }
}

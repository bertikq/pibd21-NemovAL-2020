using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AbstractTravelCompanyDatabaseImplement.Models
{
    public class TourComponent
    {
        public int Id { get; set; }

        [Required]
        public int Count { get; set; }
        public int ComponentId { get; set; }
        public virtual Component Component { get; set; }
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
    }
}

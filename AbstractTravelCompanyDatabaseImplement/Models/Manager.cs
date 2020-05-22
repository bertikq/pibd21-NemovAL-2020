using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyDatabaseImplement.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string ManagerFIO { get; set; }
        public int WorkingTime { get; set; }
        public int PauseTime { get; set; }
    }
}

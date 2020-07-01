using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.BindingModels
{
    public class ManagerBindingModel
    {
        public int? Id { get; set; }
        public string ManagerFIO { get; set; }
        public int WorkingTime { get; set; }
        public int PauseTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.ViewModels
{
    public class ManagerViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО исполнителя")]
        public string ManagerFIO { get; set; }
        [DisplayName("Время на заказ")]
        public int WorkingTime { get; set; }
        [DisplayName("Время на перерыв")]
        public int PauseTime { get; set; }
    }
}

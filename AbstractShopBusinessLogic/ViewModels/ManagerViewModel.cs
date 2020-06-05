using AbstractTravelCompanyBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.ViewModels
{
    public class ManagerViewModel : BaseViewModel
    {

        [Column(title: "ФИО исполнителя", width: 150)]
        public string ManagerFIO { get; set; }

        [Column(title: "Время на заказ", width: 100)]
        public int WorkingTime { get; set; }

        [Column(title: "Время на перерыв", width: 100)]
        public int PauseTime { get; set; }

        public override List<string> Properties() => new List<string> { "Id", "ManagerFIO", "WorkingTime", "PauseTime" };
    }
}

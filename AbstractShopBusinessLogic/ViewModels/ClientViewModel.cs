using AbstractTravelCompanyBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel : BaseViewModel
    {
        [DataMember]
        [Column(title: "ФИО", width: 150)]
        public string FIO { get; set; }

        [DataMember]
        [Column(title: "Почта", width: 150)]
        public string Email { get; set; }

        [DataMember]
        [Column(title: "Пароль", width: 150)]
        public string Password { get; set; }

        public override List<string> Properties() => new List<string> { "Id", "FIO", "Email", "Password" };
    }
}

using AbstractTravelCompanyBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractTravelCompanyBusinessLogic.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel : BaseViewModel
    {
        [DataMember]
        public string MessageId { get; set; }

        [DataMember]
        [Column(title: "Отправитель", width: 150)]
        public string SenderName { get; set; }

        [DataMember]
        [Column(title: "Дата письма", width: 150)]
        public DateTime DateDelivery { get; set; }

        [DataMember]
        [Column(title: "Заголовок", width: 150)]
        public string Subject { get; set; }

        [DataMember]
        [Column(title: "Текст", width: 150)]
        public string Body { get; set; }

        public override List<string> Properties() => new List<string> { "Id", "SenderName", "DateDelivery", "Subject", "Body" };
    }

}

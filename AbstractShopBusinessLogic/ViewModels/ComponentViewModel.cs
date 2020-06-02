using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.ViewModels
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    /// 
    [DataContract]
    public class ComponentViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DisplayName("Название компонента")]
        [DataMember]
        public string ComponentName { get; set; }
    }

}

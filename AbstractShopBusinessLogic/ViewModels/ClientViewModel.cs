﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FIO { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}

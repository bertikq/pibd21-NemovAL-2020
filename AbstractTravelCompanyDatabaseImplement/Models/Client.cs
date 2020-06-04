using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AbstractTravelCompanyDatabaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("ClientId")]
        public virtual List<Order> Orders { get; set; }

        [ForeignKey("ClientId")]
        public virtual List<MessageInfo> MessageInfos { get; set; }
    }
}

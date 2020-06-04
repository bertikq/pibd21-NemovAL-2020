using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractTravelCompanyDatabaseImplement.Implements
{
    public class MessageInfoLogic : IMessageInfoLogic
    {
        public void Create(MessageInfoBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                MessageInfo element = context.MessageInfos.FirstOrDefault(rec =>
               rec.MessageId == model.MessageId);
                if (element != null)
                {
                    throw new Exception("Уже есть письмо с таким идентификатором");
                }
                int? clientId = context.Clients.FirstOrDefault(rec => rec.Email ==
               model.FromMailAddress)?.Id;
                context.MessageInfos.Add(new MessageInfo
                {
                    MessageId = model.MessageId,
                    ClientId = clientId,
                    SenderName = model.FromMailAddress,
                    DateDelivery = model.DateDelivery,
                    Subject = model.Subject,
                    Body = model.Body
                });
                context.SaveChanges();
            }
        }
        public List<MessageInfoViewModel> Read(MessageInfoBindingModel model, int NumPage, int CountElemntsOnPage)
        {
            using (var context = new DataBaseContext())
            {
                return context.MessageInfos
                .Where(rec => model == null || rec.ClientId == model.ClientId)
                .Skip(NumPage * CountElemntsOnPage)
                .Take(CountElemntsOnPage)
                .Select(rec => new MessageInfoViewModel
                {
                    MessageId = rec.MessageId,
                    SenderName = rec.SenderName,
                    DateDelivery = rec.DateDelivery,
                    Subject = rec.Subject,
                    Body = rec.Body
                }).ToList();
            }
        }
    }
}

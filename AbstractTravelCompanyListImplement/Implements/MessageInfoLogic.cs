using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyListImplement;
using AbstractTravelCompanyListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractTravelCompanyFileImplement.Implements
{
    public class MessageInfoLogic : IMessageInfoLogic
    {
        private readonly DataListSingleton source;
        public MessageInfoLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void Create(MessageInfoBindingModel model)
        {
                MessageInfo element = source.MessageInfos.FirstOrDefault(rec =>
               rec.MessageId == model.MessageId);
                if (element != null)
                {
                    throw new Exception("Уже есть письмо с таким идентификатором");
                }
                int? clientId = source.Clients.FirstOrDefault(rec => rec.Email == model.FromMailAddress)?.Id;
                source.MessageInfos.Add(new MessageInfo
                {
                    MessageId = model.MessageId,
                    ClientId = clientId,
                    SenderName = model.FromMailAddress,
                    DateDelivery = model.DateDelivery,
                    Subject = model.Subject,
                    Body = model.Body
                });
        }
        public List<MessageInfoViewModel> Read(MessageInfoBindingModel model)
        {
                return source.MessageInfos
                .Where(rec => model == null || rec.ClientId == model.ClientId)
                .Select(rec => new MessageInfoViewModel
                {
                    MessageId = rec.MessageId,
                    SenderName = rec.SenderName,
                    DateDelivery = rec.DateDelivery,
                    Subject = rec.Subject,
                    Body = rec.Body
                })
               .ToList();
        }
    }
}

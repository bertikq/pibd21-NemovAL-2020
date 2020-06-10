using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractTravelCompanyFileImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        private readonly FileDataListSingleton source;
        public ClientLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ClientBindingModel model)
        {
            Client client = source.Clients.FirstOrDefault(c => c.Email == model.Email && c.Id != model.Id);
            if (client != null)
            {
                throw new Exception("Уже есть клиент с такой почтой");
            }

            if (model.Id.HasValue)
            {
                client = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (client == null)
                {
                    throw new Exception("Элемент не найден");
                }
                else
                {
                    client.FIO = model.ClientFIO;
                    client.Email = model.Email;
                    client.Password = model.Password;
                }
            }
            else
            {
                source.Clients.Add(new Client
                {
                    FIO = model.ClientFIO,
                    Email = model.Email,
                    Password = model.Password
                });
            }
        }

        public void Delete(ClientBindingModel model)
        {
            Client client = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (client != null)
            {
                source.Clients.Remove(client);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            List<ClientViewModel> clients = source.Clients
                   .Where(rec => model == null || rec.Id == model.Id ||
                   (rec.Email == model.Email && rec.Password == model.Password))
                   .Select(rec => new ClientViewModel
                   {
                       Email = rec.Email,
                       FIO = rec.FIO,
                       Id = rec.Id,
                       Password = rec.Password
                   }).ToList();

            if (clients.Count > 0)
                return clients;

            return null;
        }
    }
}

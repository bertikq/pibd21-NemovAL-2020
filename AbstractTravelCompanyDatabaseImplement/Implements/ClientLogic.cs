using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AbstractTravelCompanyDatabaseImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                Client client = context.Clients.FirstOrDefault(c => c.Email == model.Email && c.Id != model.Id);
                if (client != null)
                {
                    throw new Exception("Уже есть клиент с такой почтой");
                }

                if (model.Id.HasValue)
                {
                    client = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                    if (client == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    else
                    {
                        client.FIO = model.ClientFIO;
                        client.Email = model.Email;
                        client.Password = model.Password;
                        context.SaveChanges();
                    }
                }
                else
                {
                    context.Clients.Add(new Client
                    {
                        FIO = model.ClientFIO,
                        Email = model.Email,
                        Password = model.Password
                    });

                    context.SaveChanges();
                }
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                Client client = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (client != null)
                {
                    context.Clients.Remove(client);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                List<ClientViewModel> clients = context.Clients
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
}

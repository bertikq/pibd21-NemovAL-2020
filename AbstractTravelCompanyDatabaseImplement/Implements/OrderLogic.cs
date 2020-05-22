using AbstractShopBusinessLogic.BindingModels;
using AbstractShopBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AbstractShopBusinessLogic.Enums;

namespace AbstractTravelCompanyDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                Order element = new Order();
                if (model.Id.HasValue)
                {
                    element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    else
                    {
                        element.Count = model.Count;
                        element.Sum = model.Sum;
                        element.DateCreate = model.DateCreate;
                        element.DateImplement = model.DateImplement;
                        element.Status = model.Status;
                        element.TourId = model.TourId;
                        element.ClientId = model.ClientId.Value;
                        element.Client = context.Clients.FirstOrDefault(c => c.Id == model.ClientId);
                        element.ManagerId = model.ManagerId;
                    }
                }
                else
                {
                    element = new Order
                    {
                        Count = model.Count,
                        Sum = model.Sum,
                        DateCreate = model.DateCreate,
                        DateImplement = model.DateImplement,
                        Status = model.Status,
                        TourId = model.TourId,
                        ClientId = model.ClientId.Value,
                        Client = context.Clients.FirstOrDefault(c => c.Id == model.ClientId),
                        ManagerId = model.ManagerId
                };
                    context.Orders.Add(element);
                }

                context.SaveChanges();
            }
        }

        public void Delete(OrderBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }

                context.SaveChanges();
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            using (var context = new DataBaseContext())
            {
                return context.Orders
                .Include(x => x.Tour)
                .Include(x => x.Client)
                .Include(x => x.Manager)
                .Where(rec => model == null || 
                (rec.Id == model.Id && model.Id.HasValue) || 
                (dateFrom.HasValue && dateTo.HasValue && rec.DateCreate >= dateFrom && rec.DateCreate <= dateTo) || 
                (model.ClientId.HasValue && rec.ClientId == model.ClientId) || 
                (model.FreeOrders.HasValue && model.FreeOrders.Value && !rec.ManagerId.HasValue) || 
                (model.ManagerId.HasValue && rec.ManagerId == model.ManagerId && rec.Status == OrderStatus.Выполняется))
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                    Status = rec.Status,
                    TourId = rec.TourId,
                    TourName = rec.Tour.TourName,
                    ClientFIO = rec.Client.FIO,
                    ClientId = rec.ClientId,
                    ManagerFIO = rec.ManagerId.HasValue ? rec.Manager.ManagerFIO : string.Empty,
                }).ToList();
            }
        }
    }
}

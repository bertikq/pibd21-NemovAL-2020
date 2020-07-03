using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Enums;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractTravelCompanyListImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        private readonly DataListSingleton source;
        public OrderLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order tempProduct = model.Id.HasValue ? null : new Order { Id = 1 };
            foreach (var order in source.Orders)
            {
                if (order.TourId == model.TourId && order.Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (!model.Id.HasValue && order.Id >= tempProduct.Id)
                {
                    tempProduct.Id = order.Id + 1;
                }
                else if (model.Id.HasValue && order.Id == model.Id)
                {
                    tempProduct = order;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempProduct == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempProduct);
            }
            else
            {
                source.Orders.Add(CreateModel(model, tempProduct));
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {

            int max = 1;

            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == order.Id)
                {
                    Order cur = source.Orders[i];
                    cur.TourId = model.TourId;
                    cur.Count = model.Count;
                    cur.Sum = model.Sum;
                    cur.Status = model.Status;
                    cur.DateCreate = model.DateCreate;
                    cur.DateImplement = model.DateImplement;

                    return cur;
                }


                if (source.Orders[i].Id >= max)
                {
                    max = source.Orders[i].Id;
                }
            }

            order.Id = max;
            order.TourId = model.TourId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;

            return order;
        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    source.Orders.RemoveAt(i--);
                }
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            return source.Orders
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
                TourName = source.Tours.FirstOrDefault(a => a.Id == model.TourId).TourName,
                ClientFIO = source.Clients.FirstOrDefault(a => a.Id == model.ClientId).FIO,
                ClientId = rec.ClientId,
                ManagerFIO = source.Managers.FirstOrDefault(a => a.Id == model.ManagerId).ManagerFIO,
            }).ToList();
        }
    }
}

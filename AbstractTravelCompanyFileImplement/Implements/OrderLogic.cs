﻿using AbstractShopBusinessLogic.BindingModels;
using AbstractShopBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractTravelCompanyFileImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        private readonly FileDataListSingleton source;
        public OrderLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order element = new Order();
            if (model.Id.HasValue)
            {
                element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
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
                }
            }
            else
            {
                int maxId = source.Components.Count > 0 ? source.Components.Max(rec =>
               rec.Id) : 0;
                element = new Order {
                    Id = maxId + 1,
                    Count = model.Count,
                    Sum = model.Sum,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                    Status = model.Status,
                    TourId = model.TourId
                };
                source.Orders.Add(element);
            }
        }

        public void Delete(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Orders.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            return source.Orders
            .Where(rec => model == null ||
            (model.Id.HasValue && rec.Id == model.Id) ||
            (dateFrom.HasValue && dateTo.HasValue && rec.DateCreate >= dateFrom && rec.DateCreate <= dateTo) ||
            (model.ClientId.HasValue && rec.ClientId == model.ClientId))
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                Count = rec.Count,
                Sum = rec.Sum,
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement,
                Status = rec.Status,
                TourId = rec.TourId,
                TourName = source.Tours.FirstOrDefault(a => a.Id == rec.TourId)?.TourName,
                ClientId = rec.ClientId,
                ClientFIO = source.Clients.FirstOrDefault(a => a.Id == rec.ClientId).FIO
            })
            .ToList();
        }
    }
}

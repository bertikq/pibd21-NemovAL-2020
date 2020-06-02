using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractTravelCompanyFileImplement.Implements
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
            List<OrderViewModel> result = new List<OrderViewModel>();
            if (dateFrom == null || dateTo == null)
            {
                foreach (var order in source.Orders)
                {
                    if (model != null)
                    {
                        if (order.Id == model.Id)
                        {
                            result.Add(CreateViewModel(order));
                            break;
                        }
                        continue;
                    }
                    result.Add(CreateViewModel(order));
                }
            }
            else
            {
                foreach (var order in source.Orders)
                {
                    if (order.DateCreate >= dateFrom && order.DateCreate <= dateTo)
                    {
                        if (model != null)
                        {
                            if (order.Id == model.Id)
                            {
                                result.Add(CreateViewModel(order));
                                break;
                            }
                            continue;
                        }
                        result.Add(CreateViewModel(order));
                    }
                }
            }
            return result;
        }

        private OrderViewModel CreateViewModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = order.Status,
                TourId = order.TourId,
                TourName = source.Tours.FirstOrDefault(a => a.Id == order.TourId).TourName
            };
        }
    }
}

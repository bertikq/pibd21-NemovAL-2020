﻿using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Enums;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AbstractTravelCompanyBusinessLogic.BusinessLogics
{
    public class WorkModeling
    {
        private readonly IManagerLogic managerLogic;
        private readonly IOrderLogic orderLogic;
        private readonly IStoreLogic storeLogic;
        private readonly MainLogic mainLogic;
        private readonly Random rnd;
        public WorkModeling(IManagerLogic managerLogic, IOrderLogic orderLogic,
       MainLogic mainLogic, IStoreLogic storeLogic)
        {
            this.managerLogic = managerLogic;
            this.orderLogic = orderLogic;
            this.mainLogic = mainLogic;
            this.storeLogic = storeLogic;
            rnd = new Random(1000);
        }
        /// <summary>
        /// Запуск работ
        /// </summary>
        public void DoWork()
        {
            var managers = managerLogic.Read(null);
            var orders = orderLogic.Read(new OrderBindingModel { FreeOrders = true });
            foreach (var manager in managers)
            {
                WorkerWorkAsync(manager, orders);
            }
        }
        /// <summary>
        /// Иммитация работы исполнителя
        /// </summary>
        /// <param name="implementer"></param>
        /// <param name="orders"></param>
        private async void WorkerWorkAsync(ManagerViewModel implementer, List<OrderViewModel> orders)
        {
            // ищем заказы, которые уже в работе (вдруг исполнителя прервали)
            var runOrders = await Task.Run(() => orderLogic.Read(new OrderBindingModel
            {
                ManagerId = implementer.Id,
                Status = Enums.OrderStatus.Выполняется
            }));
            foreach (var order in runOrders)
            {
                // делаем работу заново
                Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);
                mainLogic.FinishOrder(new ChangeStatusBindingModel
                {
                    OrderId = order.Id
                });
                // отдыхаем
                Thread.Sleep(implementer.PauseTime);
            }

            var ordersWithoutMaterials = await Task.Run(() => orderLogic.Read(new OrderBindingModel()
            {
                ManagerId = implementer.Id,
                Status = OrderStatus.ТребуютсяМатериалы
            }));

            await Task.Run(() =>
            {
                foreach (var order in ordersWithoutMaterials)
                {
                    //пытаемся опять взять заказ в работу
                    mainLogic.TakeOrderInWork(new ChangeStatusBindingModel()
                    {
                        OrderId = order.Id,
                        ManagerId = implementer.Id
                    });
                    var updatedOrder = orderLogic.Read(new OrderBindingModel() { Id = order.Id })[0];
                    if (updatedOrder.Status != Enums.OrderStatus.ТребуютсяМатериалы)
                    {
                        // делаем работу 
                        Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);
                        mainLogic.FinishOrder(new ChangeStatusBindingModel
                        {
                            OrderId = order.Id
                        });
                        // отдыхаем
                        Thread.Sleep(implementer.PauseTime);
                    }
                }
            });

            await Task.Run(() =>
            {
                foreach (var order in orders)
                {
                    // пытаемся назначить заказ на исполнителя
                    try
                    {
                        mainLogic.TakeOrderInWork(new ChangeStatusBindingModel
                        {
                            OrderId = order.Id,
                            ManagerId = implementer.Id
                        });
                        // делаем работу
                        Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);

                        mainLogic.FinishOrder(new ChangeStatusBindingModel
                         {
                             OrderId =  order.Id,
                             ManagerId = implementer.Id
                         });

                        Thread.Sleep(implementer.PauseTime);
                    }
                    catch (Exception) {
                    }
                }
            });
        }
    }
}

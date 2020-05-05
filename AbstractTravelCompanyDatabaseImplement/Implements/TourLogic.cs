using AbstractShopBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractTravelCompanyDatabaseImplement.Implements
{
    public class TourLogic : ITourLogic
    {
        public void CreateOrUpdate(TourBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Tour element = context.Tours.FirstOrDefault(rec =>
                        rec.TourName == model.TourName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть изделие с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Tours.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Tour();
                            context.Tours.Add(element);
                        }
                        element.TourName = model.TourName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var productComponents = context.TourComponents.Where(rec
                                => rec.TourId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.TourComponents.RemoveRange(productComponents.Where(rec =>
                            !model.TourComponents.ContainsKey(rec.ComponentId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateComponent in productComponents)
                            {
                                updateComponent.Count =
                               model.TourComponents[updateComponent.ComponentId].Item2;

                                model.TourComponents.Remove(updateComponent.ComponentId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.TourComponents)
                        {
                            context.TourComponents.Add(new TourComponent
                            {
                                TourId = element.Id,
                                ComponentId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(TourBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.TourComponents.RemoveRange(context.TourComponents.Where(rec =>
                        rec.TourId == model.Id));
                        Tour element = context.Tours.FirstOrDefault(rec => rec.Id
                        == model.Id);
                        if (element != null)
                        {
                            context.Tours.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<TourViewModel> Read(TourBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                return context.Tours
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new TourViewModel
               {
                   Id = rec.Id,
                   TourName = rec.TourName,
                   Price = rec.Price,
                   ProductComponents = context.TourComponents
                .Include(recPC => recPC.Component)
                .Where(recPC => recPC.TourId == rec.Id)
                .ToDictionary(recPC => recPC.ComponentId, recPC =>
                (recPC.Component?.ComponentName, recPC.Count))
               }).ToList();
            }
        }
    }
}


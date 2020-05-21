using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractTravelCompanyDatabaseImplement.Implements
{
    public class StoreLogic : IStoreLogic
    {
        private readonly ITourLogic _tourLogic;

        public StoreLogic(ITourLogic tourLogic)
        {
            _tourLogic = tourLogic;
        }

        public void AddComponent(AddComponentInStoreBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                StoreComponent storeComponent = context.StoreComponents.FirstOrDefault(x =>
                      x.StoreId == model.StoreId && x.ComponentId == model.ComponentId);

                if (storeComponent == null)
                {
                    context.StoreComponents.Add(new StoreComponent
                    {
                        Count = model.Count,
                        ComponentId = model.ComponentId,
                        StoreId = model.StoreId
                    });
                    context.SaveChanges();
                    return;
                }

                storeComponent.Count += model.Count;

                context.SaveChanges();
            }
        }

        public void CreateOrUpdate(StoreBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                Store curStore = context.Stores.FirstOrDefault(x => x.Id == model.Id);

                if (model.Name == "")
                    throw new Exception("Название не может быть пустым");

                if (curStore == null)
                {

                    if (context.Stores.FirstOrDefault(x => x.Name == model.Name) != null)
                    {
                        throw new Exception("Уже есть склад с таким названием");
                    }

                    context.Stores.Add(new Store
                    {
                        Name = model.Name
                    });
                    context.SaveChanges();
                    return;
                }

                curStore.Name = model.Name;
                context.SaveChanges();
            }
        }

        public void Delete(StoreBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
            }
        }

        public List<StoreViewModel> Read(StoreBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                return context.Stores
               .Where(rec => model == null || rec.Id == model.Id)
               .ToList()
               .Select(rec => new StoreViewModel
               {
                   Id = rec.Id,
                   Name = rec.Name,
                   StoreComponents = context.StoreComponents
                   .Include(x => x.Component)
                   .Where(sp => sp.StoreId == rec.Id)
                   .ToDictionary(recPC => recPC.ComponentId, recPC => 
                   (recPC.Component?.ComponentName, recPC.Count))
               })
               .ToList();
            }
        }

        public void WriteOffComponents(int componentId, int count)
        {
            using (var context = new DataBaseContext())
            {
                foreach (StoreComponent storeComponent in context.StoreComponents.Where(sc => sc.ComponentId == componentId))
                {
                    if (count > 0)
                    {
                        if (storeComponent.Count >= count)
                        {
                            storeComponent.Count -= count;
                            count = 0;
                        }
                        else
                        {
                            count -= storeComponent.Count;
                            storeComponent.Count = 0;
                        }
                    }
                }

                context.StoreComponents.RemoveRange(context.StoreComponents.Where(x => x.Count == 0));
            }
        }


        public bool IsWriteOffComponents(int componentId, int count)
        {
            using (var context = new DataBaseContext())
            {
                int curCount = 0;
                curCount = context.StoreComponents.Where(sc => sc.ComponentId == componentId).Sum(x => x.Count);

                if (curCount < count)
                {
                    return false;
                }

                return true;
            }
        }

        public void WriteOffTour(int tourId, int countTours)
        {

            using (var context = new DataBaseContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var tour = _tourLogic.Read(new TourBindingModel
                        {
                            Id = tourId
                        })?[0];

                        if (tour == null)
                        {
                            throw new Exception("Не найден тур");
                        }
                        foreach (var componentId in tour.ProductComponents.Keys)
                        {
                            int countComponent = tour.ProductComponents[componentId].Item2;
                            foreach (StoreComponent storeComponent in context.StoreComponents.Where(sc => sc.ComponentId == componentId))
                            {
                                if (countComponent > 0)
                                {
                                    if (storeComponent.Count >= countComponent)
                                    {
                                        storeComponent.Count -= countComponent;
                                        countComponent = 0;
                                    }
                                    else
                                    {
                                        countComponent -= storeComponent.Count;
                                        storeComponent.Count = 0;
                                    }
                                }
                            }

                            if (countComponent > 0)
                            {
                                throw new Exception("Недостаточно компонентов на складе");
                            }
                        }
                        foreach (var sc in context.StoreComponents)
                        {
                            if (sc.Count == 0)
                            {
                                context.StoreComponents.Remove(sc);
                            }
                        }

                        

                        context.SaveChanges();
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
    }
}

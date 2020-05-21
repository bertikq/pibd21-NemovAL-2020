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
    public class StoreLogic : IStoreLogic
    {
        private readonly ITourLogic _tourLogic;
        private readonly FileDataListSingleton source;
        public StoreLogic(ITourLogic tourLogic)
        {
            _tourLogic = tourLogic;
            source = FileDataListSingleton.GetInstance();
        }
        public void AddComponent(AddComponentInStoreBindingModel model)
        {
            StoreComponent storeComponent = source.StoreComponents.FirstOrDefault(x =>
                   x.StoreId == model.StoreId && x.ComponentId == model.ComponentId);

            if (storeComponent == null)
            {
                int? newId = source.Stores.Max(s => (int?)s.Id) + 1;
                if (!newId.HasValue)
                    newId = 0;

                source.StoreComponents.Add(new StoreComponent
                {
                    Id = newId.Value,
                    Count = model.Count,
                    ComponentId = model.ComponentId,
                    StoreId = model.StoreId
                });
                return;
            }

            storeComponent.Count += model.Count;
        }

        public void CreateOrUpdate(StoreBindingModel model)
        {
            Store curStore = source.Stores.FirstOrDefault(x => x.Id == model.Id);

            if (model.Name == "")
                throw new Exception("Название не может быть пустым");

            if (curStore == null)
            {

                if (source.Stores.FirstOrDefault(x => x.Name == model.Name) != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }

                int? newId = source.Stores.Max(s => (int?)s.Id) + 1;
                if (!newId.HasValue)
                    newId = 0;

                source.Stores.Add(new Store
                {
                    Id = newId.Value,
                    Name = model.Name
                });
                return;
            }

            curStore.Name = model.Name;
        }

        public void Delete(StoreBindingModel model)
        {
            Store curStore = source.Stores.FirstOrDefault(x => x.Id == model.Id);

            if (curStore == null)
            {
                throw new Exception("Элемент не найден");
            }

            source.Stores.Remove(curStore);

            source.StoreComponents.RemoveAll(sp => sp.StoreId == curStore.Id);
        }

        public List<StoreViewModel> Read(StoreBindingModel model)
        {
            return source.Stores
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new StoreViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                StoreComponents = source.StoreComponents
                .Where(sp => sp.StoreId == rec.Id)
                .ToDictionary(recPC => recPC.ComponentId, recPC =>
                (source.Components.FirstOrDefault(recC => recC.Id ==
                recPC.ComponentId)?.ComponentName, recPC.Count))
            })
            .ToList();
        }

        public bool IsWriteOffComponents(int componentId, int count)
        {
            int curCount = 0;
            curCount = source.StoreComponents.Where(sc => sc.ComponentId == componentId).Sum(x => x.Count);

            if (curCount < count)
            {
                return false;
            }

            return true;
        }

        public void WriteOffComponents(int componentId, int count)
        {
            foreach (StoreComponent storeComponent in source.StoreComponents.Where(sc => sc.ComponentId == componentId))
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

            source.StoreComponents.RemoveAll(sc => sc.Count == 0);
        }

        public void WriteOffTour(int tourId, int count)
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
                foreach (StoreComponent storeComponent in source.StoreComponents.Where(sc => sc.ComponentId == componentId))
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

            source.StoreComponents.RemoveAll(x => x.Count == 0);
        }
    }
}

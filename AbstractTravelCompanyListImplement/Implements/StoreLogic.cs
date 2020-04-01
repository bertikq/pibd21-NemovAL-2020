using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractTravelCompanyListImplement.Implements
{
    public class StoreLogic : IStoreLogic
    {
        private readonly DataListSingleton source;
        public StoreLogic()
        {
            source = DataListSingleton.GetInstance();
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

                int newId = 0;
                while (true)
                {
                    bool isNewId = true;
                    foreach(Store store in source.Stores)
                    {
                        if (store.Id == newId)
                        {
                            newId++;
                            isNewId = false;
                            break;
                        }
                    }

                    if (isNewId)
                    {
                        source.Stores.Add(new Store
                        {
                            Id = newId,
                            Name = model.Name
                        });
                        model.Id = newId;
                        UpdateStoreComponents(model);
                        return;
                    }
                }
            }

            curStore.Name = model.Name;
            UpdateStoreComponents(model);
        }

        public void Delete(StoreBindingModel model)
        {
            Store curStore = source.Stores.FirstOrDefault(x => x.Id == model.Id);

            if (curStore == null)
            {
                throw new Exception("Элемент не найден");
            }

            source.Stores.Remove(curStore);

            for (int i = 0; i < source.StoreComponents.Count; i++)
            {
                if (source.StoreComponents[i].StoreId == curStore.Id)
                {
                    source.StoreComponents.RemoveAt(i);
                }
            }
        }

        public List<StoreViewModel> Read(StoreBindingModel model)
        {
            List<StoreViewModel> result = new List<StoreViewModel>();
            foreach (var component in source.Stores)
            {
                if (model != null)
                {
                    if (component.Id == model.Id)
                    {
                        result.Add(CreateStoreViewModel(component));
                        break;
                    }
                    continue;
                }
                result.Add(CreateStoreViewModel(component));
            }
            return result;
        }

        private StoreViewModel CreateStoreViewModel(Store model)
        {
            StoreViewModel storeViewModel = new StoreViewModel
            {
                Id = model.Id,
                Name = model.Name,
                StoreComponents = new Dictionary<int, (string, int)>()
            };

            foreach(StoreComponent storeComponent in source.StoreComponents)
            {
                if (storeComponent.StoreId == model.Id) {
                    storeViewModel.StoreComponents.Add(
                        storeComponent.ComponentId,
                        (source.Components.FirstOrDefault(x => x.Id == storeComponent.ComponentId).ComponentName,
                        storeComponent.Count));
                }
            }

            return storeViewModel;
        }

        private void UpdateStoreComponents(StoreBindingModel model)
        {
            if (model.StoreComponents == null)
            {
                return;
            }

            List<StoreComponent> storeComponents = source.StoreComponents.Where(x => x.StoreId == model.Id).ToList();

            for (int i = 0; i < storeComponents.Count; i++)
            {
                if (!model.StoreComponents.ContainsKey(storeComponents[i].ComponentId))
                {
                    storeComponents.RemoveAt(i);
                }
            }

            foreach (int idComponent in model.StoreComponents.Keys)
            {
                bool isHave = false;

                foreach (StoreComponent storeComponent in storeComponents)
                {
                    if (idComponent == storeComponent.ComponentId)
                    {
                        isHave = true;
                        storeComponent.Count = model.StoreComponents[idComponent].Item2;
                        break;
                    }
                }

                if (!isHave)
                {
                    int newId = 0;
                    while (true)
                    {
                        bool isNewId = true;
                        foreach (StoreComponent storeComponent in source.StoreComponents)
                        {
                            if (storeComponent.Id == newId)
                            {
                                newId++;
                                isNewId = false;
                                break;
                            }
                        }

                        if (isNewId)
                            break;
                    }

                    source.StoreComponents.Add(new StoreComponent
                    {
                        ComponentId = idComponent,
                        Count = model.StoreComponents[idComponent].Item2,
                        Id = newId,
                        StoreId = model.Id.Value
                    });
                }
            }
        }
    }
}

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
    public class ManagerLogic : IManagerLogic
    {
        private readonly DataListSingleton source;
        public ManagerLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ManagerBindingModel model)
        {
            Manager element = source.Managers.FirstOrDefault(c => c.ManagerFIO == model.ManagerFIO && c.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть менеджер с таким именем");
            }
            if (model.Id.HasValue)
            {
                element = source.Managers.FirstOrDefault(rec => rec.Id == model.Id);

                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                else
                {
                    element.ManagerFIO = model.ManagerFIO;
                    element.PauseTime = model.PauseTime;
                    element.WorkingTime = model.WorkingTime;
                }
            }
            else
            {
                element = new Manager
                {
                    ManagerFIO = model.ManagerFIO,
                    PauseTime = model.PauseTime,
                    WorkingTime = model.WorkingTime
                };
                source.Managers.Add(element);
            }
        }

        public void Delete(ManagerBindingModel model)
        {
            Manager element = source.Managers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Managers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<ManagerViewModel> Read(ManagerBindingModel model)
        {
            return source.Managers
               .Where(rec => model == null || rec.Id == model.Id)
               .Select(rec => new ManagerViewModel
               {
                   Id = rec.Id,
                   WorkingTime = rec.WorkingTime,
                   PauseTime = rec.PauseTime,
                   ManagerFIO = rec.ManagerFIO
               })
               .ToList();
        }
    }
}

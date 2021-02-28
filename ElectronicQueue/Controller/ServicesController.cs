using System;
using System.Collections.Generic;
using ElectronicQueue.Model.Services;
using ElectronicQueue.Model.Услуги;

namespace ElectronicQueue.Controller
{
    public class ServicesController : IServicesController
    {
        private readonly Dictionary<string, IService> _services = new Dictionary<string, IService>();

        /// <summary>
        /// Создание новой услуги. 
        /// </summary>
        /// <param name="name">Название услуги</param>
        /// <param name="estimationTime">Плановое время</param>
        /// <returns>True  - если услуга создана. False - Если услуга уже существует </returns>
        public bool Add(string name, int estimationTime)
        {
            try
            {
                _services.Add(name, new Service(name, estimationTime));
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Удаление услуги
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Remove(string name)
        {
            try
            {
                _services.Remove(name);
            }
            catch
            {
                Console.WriteLine("Услуга не найдена!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Возвращает Обьект услуги, если такой имеется. Иначе null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IService Get(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            var isSuccess = _services.TryGetValue(name, out var service);

            return isSuccess ? service : null;
        }

        /// <summary>
        /// Список доступных Услуг
        /// </summary>
        /// <returns>Коллекция услуг</returns>
        public Dictionary<string, IService> Get()
        {
            return _services;
        }
    }

    public interface IServicesController
    {
        /// <summary>
        /// Создание новой услуги. 
        /// </summary>
        /// <param name="name">Название услуги</param>
        /// <param name="estimationTime">Плановое время</param>
        /// <returns>True  - если услуга создана. False - Если услуга уже существует </returns>
        bool Add(string name, int estimationTime);

        /// <summary>
        /// Удаление услуги
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool Remove(string name);

        /// <summary>
        /// Возвращает Обьект услуги, если такой имеется. Иначе null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IService Get(string name);

        /// <summary>
        /// Список доступных Услуг
        /// </summary>
        /// <returns>Коллекция услуг</returns>
        Dictionary<string, IService> Get();
    }
}

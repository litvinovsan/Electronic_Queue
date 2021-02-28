﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicQueue.Model;
using ElectronicQueue.Model.Tickets;
using ElectronicQueue.Model.Windows;
using Console = System.Console;

namespace ElectronicQueue.Controller
{
    public class WindowsController : IWindowsController
    {
        public WindowsController()
        {
            WndDictionary = new Dictionary<string, IWindow>();
        }

        /// <summary>
        /// Создание окна
        /// </summary>
        /// <param name="name"></param>
        public void Add(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            WndDictionary.Add(name, new Window(name));
        }

        public void AddTicket(string windowName, ITicket ticket)
        {
            try
            {
                WndDictionary[windowName].AddTicket(ticket);
            }
            catch 
            {
                Console.WriteLine("Ошибка добавления");
            }
        }

        /// <summary>
        /// Возвращает обьект "окна" с заданным именем, если окно с таким именем существует
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IWindow GetWindow(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            return WndDictionary.TryGetValue(name, out var resultWindow) ? resultWindow : null;
        }

        public List<string> GetWindowsName()
        {
            return WndDictionary.Keys.ToList();
        }

        /// <summary>
        /// Доступ к Окнам через индексатор-Имя окна
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IWindow this[string name]
        {
            get { return GetWindow(name); }
            set
            {
                if (string.IsNullOrEmpty(name)) return;

                if (WndDictionary.ContainsKey(name))
                {
                    WndDictionary[name] = value;
                }
                else
                {
                    WndDictionary.Add(name, value);
                }
            }
        }

        public Dictionary<string, IWindow> WndDictionary { get; }
    }

    public interface IWindowsController
    {
        /// <summary>
        /// Создание окна
        /// </summary>
        /// <param name="name"></param>
        void Add(string name);

        void AddTicket(string windowName, ITicket ticket);
        /// <summary>
        /// Возвращает обьект "окна" с заданным именем, если окно с таким именем существует
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IWindow GetWindow(string name);

        List<string> GetWindowsName();

        /// <summary>
        /// Доступ к Окнам через индексатор-Имя окна
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IWindow this[string name] { get; set; }

        Dictionary<string, IWindow> WndDictionary { get; }
    }

}

﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bionessori.Models;

namespace Bionessori.Controllers {
    /// <summary>
    /// Контроллер описывает работу с роутами ERP-системы.
    /// </summary>
    public class RouteController : Controller {
        private readonly ILogger<RouteController> _logger;

        public RouteController(ILogger<RouteController> logger) {
            _logger = logger;
        }

        // Метод перенаправляет на главную страницу ведения объектов.
        public IActionResult Index() {
            ViewData["Title"] = "Ведение объектов";

            return View();
        }

        // Метод перенаправляет на страницу со списком карт пациентов.
        public IActionResult RouteCard() {
            ViewData["Title"] = "Электронные медицинские карты пациентов - Список карт пациентов";

            return View();
        }

        // Метод переходит на страницу администратора.
        public IActionResult RouteAdmin() {
            ViewData["Title"] = "Панель администратора";

            return View();
        }

        // Метод переходи на страницу MRP.
        public IActionResult RouteMRP() {
            ViewData["Title"] = "Планирование потребности в материалах";

            return View();
        }
    }
}

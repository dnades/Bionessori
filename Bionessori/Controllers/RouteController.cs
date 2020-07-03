using System;
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
        [Route("route/card")]
        public IActionResult RouteCard() {
            ViewData["Title"] = "Электронные медицинские карты пациентов - Список карт пациентов";

            return View();
        }

        // Метод переходит на страницу администратора.
        [Route("route/admin")]
        public IActionResult RouteAdmin() {
            ViewData["Title"] = "Панель администратора";

            return View();
        }

        // Метод переходи на страницу MRP.
        [Route("route/mrp")]
        public IActionResult RouteMRP() {
            ViewData["Title"] = "Планирование потребностей в материалах";

            return View();
        }

        // Метод переходит на страницу со списком всех заявок в MRP.
        [Route("view/request")]
        public IActionResult GetRequests() {
            ViewData["Title"] = "Планирование потребностей в материалах - Список заявок";

            return View();
        }

        // Метод переходит на страницу со списком материалов.
        [Route("view/material")]
        public IActionResult GetMaterials() {
            ViewData["Title"] = "Планирование потребностей в материалах - Список материалов на складах";

            return View();
        }

        // Переходит к созданию новой заявки на потребности в материалах.
        [Route("create/request")]
        public IActionResult CreateRequest() {
            ViewData["Title"] = "Планирование потребностей в материалах - Создание новой заявки";

            return View();
        }

        // Переходит на страницу создания карты пациента.
        [Route("route/create-card")]
        public IActionResult RouteCreateCard() {
            ViewData["Title"] = "Ведение электронных карт пациентов - Создание новой карты пациента";

            return View();
        }
    }
}

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

        // Метод переходит на страницу MRP.
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
        [Route("route/create-request")]
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

        // Переходит на страницу просмотра карты пациента.
        [Route("route/get-card")]
        public IActionResult RouteGetCard() {
            ViewData["Title"] = "Ведение электронных карт пациентов - Просмотр карты пациента";

            return View();
        }

        // Переходит на страницу редактирования карты пациента.
        [Route("route/edit-card")]
        public IActionResult RouteEditCard() {
            ViewData["Title"] = "Ведение электронных карт пациентов - Редактирование карты пациента";

            return View();
        }

        // Метод переходит на страницу просмотра заявки.
        [Route("view-request")]
        public IActionResult GetRequest() {
            ViewData["Title"] = "Планирование потребностей в материалах - Просмотр заявки";

            return View();
        }

        // Метод переходит на страницу редактирования заявки.
        [Route("edit-request")]
        public IActionResult RouteEditRequest() {
            ViewData["Title"] = "Планирование потребностей в материалах - Редактирование заявки";

            return View();
        }
    }
}

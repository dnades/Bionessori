﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bionessori.Models;
using Bionessori.Core;
using Bionessori.Core.Interfaces;
using System.Security.Policy;

namespace Bionessori.Controllers {
    /// <summary>
    /// Контроллер описывает работу с роутами ERP-системы.
    /// </summary>
    public class RouteController : Controller {
        // Метод перенаправляет на главную страницу ведения объектов.
        public IActionResult Index() {
            //ViewData["Title"] = "Главная страница - Ведение объектов";

            return View();
        }

        // Метод возвращает на главную страницу.
        [Route("index")]
        public IActionResult RouteIndex() {
            return View("Index");
        }

        // Метод перенаправляет на страницу со списком карт пациентов.       
        [Route("card")]
        public IActionResult RouteCard() {
            ViewData["Title"] = "Электронные медицинские карты пациентов - Список карт пациентов";

            return View();
        }

        // Метод переходит на страницу администратора.
        [Route("admin")]
        public IActionResult RouteAdmin() {
            ViewData["Title"] = "Панель администратора";

            return View();
        }

        // Метод переходит на страницу MRP.
        [Route("mrp")]
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
            ViewData["Title"] = "Ведение объектов номенклатуры";

            return View();
        }

        // Переходит к созданию новой заявки на потребности в материалах.
        [Route("create-request")]
        public IActionResult CreateRequest() {
            ViewData["Title"] = "Планирование потребностей в материалах - Создание новой заявки";

            return View();
        }

        // Переходит на страницу создания карты пациента.
        [Route("create-card")]
        public IActionResult RouteCreateCard() {
            ViewData["Title"] = "Ведение электронных карт пациентов - Создание новой карты пациента";

            return View();
        }

        // Переходит на страницу просмотра карты пациента.
        [Route("get-card")]
        public IActionResult RouteGetCard() {
            ViewData["Title"] = "Ведение электронных карт пациентов - Просмотр карты пациента";

            return View();
        }

        // Переходит на страницу редактирования карты пациента.
        [Route("edit-card")]
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

        // Метод переходит на страницу регистратуры.
        [Route("registry")]
        public IActionResult RouteRegistry() {
            ViewData["Title"] = "Ведение объектов регистратуры";

            return View();
        }

        // Метод переходит на страницу записи пациента на прием к врачу.
        [Route("add-registry")]
        public IActionResult AddRegistry() {
            ViewData["Title"] = "Ведение объектов регистратуры - Запись пациента на прием";

            return View();
        }

        // Метод переходит на страницу редактирования записи.
        [Route("edit-reception")]
        public IActionResult EditReception() {
            ViewData["Title"] = "Ведение объектов регистратуры - Редактирование записи пациента на прием";

            return View();
        }

        // Метод переходит на страницу направлений.
        [Route("direction")]
        public IActionResult RouteDirection() {
            ViewData["Title"] = "Ведение объектов регистратуры - Направления";

            return View();
        }

        // Метод переходит на страницу личного кабинета.
        [Route("front-office")]
        public IActionResult RouteFrontOffice() {
            ViewData["Title"] = "Мой профиль";

            return View();
        }

        // Метод переходит на страницу создания направления.
        [Route("create-direction")]
        public IActionResult RouteCreateDirection() {
            ViewData["Title"] = "Ведение объектов регистратуры - Создание направления";

            return View();
        }

        // Метод переходит на страницу редактирования направления.
        [Route("edit-direction")]
        public IActionResult RouteEditDirection() {
            ViewData["Title"] = "Ведение объектов регистратуры - Редактирование направления";

            return View();
        }
       
        // Метод переходит на страницу управления закупками.
        [Route("view-manage-purchases")]
        public IActionResult RouteManagePurchases() {
            ViewData["Title"] = "Ведение объектов управления закупками";

            return View();
        }
        
        // Метод создает номенклатуру.
        [Route("create-nomenclature")]
        public IActionResult CreateNomenclature() {
            ViewData["Title"] = "Номенклатура - Создание номенклатуры";

            return View();
        }

        // Метод переходит на страницу коммерческих предложений.
        [Route("commerce-offer")]
        public IActionResult CommerceOffer() {
            ViewData["Title"] = "Управление закупками - КП (коммерческие предложения)";

            return View();
        }

        // Метод создает коммерческое предложение поставщику.
        [Route("create-commerce-offer")]
        public IActionResult CreateCommerceOffer() {
            ViewData["Title"] = "Управление закупками - Создание коммерческого предложения";

            return View();
        }
    }
}

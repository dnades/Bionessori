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

        /// <summary>
        /// Метод перенаправляет на главную страницу ведения объектов.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() {
            return View();
        }
    }
}

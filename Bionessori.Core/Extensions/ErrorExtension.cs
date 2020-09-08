using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Web.Mvc;

namespace Bionessori.Core.Extensions {
    /// <summary>
    /// Класс расширяющий тексты ошибок.
    /// </summary>
    public class ErrorExtension : Controller {
        // Если заявка уже находится в статусе "В работе".
        public JsonResult ThrowErrorReqNotWork() {
            return Json(new { HttpStatusCode.BadRequest, responseText = "Заявка уже находится в статусе 'В работе.'" });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core;
using Bionessori.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionessori.Controllers {
    /// <summary>
    /// Контроллер описывает методы работы с различными шаблонами.
    /// </summary>
    [ApiController, Route("api/template")]
    public class TemplateController : ControllerBase {
        ApplicationDbContext _db;        

        public TemplateController(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Метод получает список новых заявок.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("get-new-requests")]
        public async Task<IActionResult> GetDataNewRequests() {
            BaseTemplate template = new DynamicDataTemplateService(_db);
            var oRequests = await template.GetDynamicDataNewRequests();

            return Ok(oRequests);
        }

        /// <summary>
        /// Метод получает список заявок в работе.
        /// </summary>
        [HttpGet, Route("get-inwork-requests")]
        public async Task<IActionResult> GetDataInWorkRequests() {
            BaseTemplate template = new DynamicDataTemplateService(_db);
            var oRequests = await template.GetDynamicDataWorkRequests();
            
            return Ok(oRequests);
        }

        /// <summary>
        /// Метод получает заявки, которые ожидают подтверждения удаления.
        /// </summary>
        [HttpGet, Route("get-accept-delete-requests")]
        public async Task<IActionResult> GetAcceptDeleteRequests() {
            BaseTemplate template = new DynamicDataTemplateService(_db);
            var oRequests = await template.GetDynamicDataAcceptDeleteRequests();

            return Ok(oRequests);
        }
    }
}

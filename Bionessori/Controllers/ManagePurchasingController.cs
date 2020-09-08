using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core;
using Bionessori.Core.Data;
using Bionessori.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionessori.Controllers {
    /// <summary>
    /// Контроллер описывает методы работы отдела закупок.
    /// </summary>
    [ApiController, Route("api/purchase")]
    public class ManagePurchasingController : ControllerBase {
        ApplicationDbContext _db;
        public ManagePurchasingController(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Метод получает список заявок в статусах: в работе, выполнена, подтверждена.
        /// </summary>
        [HttpGet, Route("get-requests")]
        public async Task<IActionResult> GetRequests() {
            BasePurchases basePurchases = new PurchasesService(_db);

            return Ok(await basePurchases.GetRequests());
        }
    }
}

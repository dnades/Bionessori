using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionessori.Controllers {
    /// <summary>
    /// Контроллер описывает работу складов.
    /// </summary>
    [ApiController, Route("api/werehouse/product")]
    public class WerehouseController : ControllerBase {
        IWerehouse _werehouse;

        public WerehouseController(IWerehouse werehouse) {
            _werehouse = werehouse;
        }

        [HttpPost, Route("get-products")]
        public async Task<IActionResult> GetProducts() {
            var oProducts = await _werehouse.GetProducts();

            return Ok();
        }
    }
}

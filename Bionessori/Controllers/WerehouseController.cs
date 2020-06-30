using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionessori.Controllers {
    /// <summary>
    /// Контроллер описывает работу складов.
    /// </summary>
    [ApiController, Route("api/werehouse/material")]
    public class WerehouseController : ControllerBase {
        IWerehouse _werehouse;

        public WerehouseController(IWerehouse werehouse) {
            _werehouse = werehouse;
        }

        /// <summary>
        /// Метод получает список материалов со складов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-materials")]
        public async Task<IActionResult> GetProducts() {
            var oMaterials = await _werehouse.GetMaterials();

            return Ok(oMaterials);
        }

        /// <summary>
        /// Метод получает список кодов и названий складов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-werehouses")]
        public async Task<IActionResult> GetNameWerehouses() {
            var oWerehouses = await _werehouse.GetNameWerehouses();

            return Ok(oWerehouses);
        }
    }
}

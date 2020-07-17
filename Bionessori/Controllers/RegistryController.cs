using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionessori.Controllers {
    /// <summary>
    /// Контроллер описывает работу регистратуры.
    /// </summary>
    [ApiController, Route("api/data/registry")]
    public class RegistryController : Controller {
        IRegistry _registry;

        public RegistryController(IRegistry registry) {
            _registry = registry;
        }
        /// <summary>
        /// Метод получает список номеров карт пациентов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-numbers-cards")]
        public async Task<IActionResult> GetNumbersCards() {
            var oNumbers = await _registry.LoadCardsNumbers();

            return Ok(oNumbers);
        }
    }
}

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

        /// <summary>
        /// Метод проверяет существование пациента и если он есть, то возвращает номер его карты.
        /// </summary>
        [HttpPost, Route("identity-patient")]
        public async Task<IActionResult> IdentityPatient(PatientCard patientCard) {
            var isPatient = await _registry.GetIdentityPatient(patientCard);

            return Ok(isPatient);
        }
    }
}

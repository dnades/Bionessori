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
    /// Контроллер описывает работу с картами пациентов.
    /// </summary>
    [ApiController, Route("api/data/card")]
    public class CardController : Controller {
        ICard _card;

        public CardController(ICard card) {
            _card = card;
        }

        /// <summary>
        /// Метод получает список всех карт пациентов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-cards")]
        public async Task<IActionResult> GetCards() {
            var oCards = await _card.Take();

            return Ok(oCards);
        }

        /// <summary>
        /// Метод изменяет карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        [HttpPost, Route("update-card")]
        public async Task<IActionResult> Edit([FromBody] PatientCard patientCard) {
            await _card.Edit(patientCard);

            return Ok("Карта пациента успешно изменена");
        }

        /// <summary>
        /// Метод удаляет карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        [HttpPost, Route("delete-card")]
        public async Task<IActionResult> Delete([FromBody] PatientCard patientCard) {
            await _card.Delete(patientCard);

            return Ok("Карта пациента успешно удалена.");
        }
    }
}

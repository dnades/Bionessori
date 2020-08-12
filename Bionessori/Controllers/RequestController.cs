using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionessori.Controllers {
    [ApiController, Route("api/werehouse/request")]
    public class RequestController : ControllerBase {
        IRequest _request;

        public RequestController(IRequest request) {
            _request = request;
        }
        /// <summary>
        /// Метод получает список заявок на потребности.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("get-requests")]
        public async Task<IActionResult> GetRequests() {
            var oRequests = await _request.GetRequests();

            return Ok(oRequests);
        }

        /// <summary>
        /// Метод создает новую заявку на потребность в материалах.
        /// </summary>
        /// <param name="werehouse"></param>
        /// <returns></returns>
        [HttpPost, Route("create-request")]
        public async Task<IActionResult> Create([FromBody] Request request) {
            await _request.Create(request);

            return Ok("Заявка на потребность успешно создана");
        }

        /// <summary>
        /// Метод сохраняет изменения в заявке.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("save-change-request")]
        public async Task<IActionResult> SaveChangeRequest([FromBody] Request request) {
            await _request.Edit(request);

            return Ok("Заявка успешно изменена");
        }

        /// <summary>
        /// Метод удаляет заявку.
        /// </summary>
        [HttpPost, Route("delete-request")]
        public async Task<IActionResult> DeleteRequest([FromBody] Request request) {
            await _request.Delete(request.Number);

            return Ok("Заявка успешно удалена");
        }
    }
}

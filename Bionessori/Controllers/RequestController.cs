using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> GetRequests([FromBody] Request request) {
            var oRequests = await _request.GetRequests(request);

            return Ok(oRequests);
        }
    }
}

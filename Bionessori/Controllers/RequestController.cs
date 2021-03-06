﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bionessori.Core;
using Bionessori.Core.Data;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Bionessori.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Bionessori.Controllers {
    [ApiController, Route("api/werehouse/request")]
    public class RequestController : ControllerBase {
        ApplicationDbContext _db;
        public RequestController(ApplicationDbContext db) {
            _db = db;
        }
        /// <summary>
        /// Метод получает список заявок на потребности.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("get-requests")]
        public async Task<IActionResult> GetRequests() {
            BaseRequest baseRequest = new RequestService(_db);
            return Ok(await baseRequest.GetRequests());
        }

        /// <summary>
        /// Метод создает новую заявку на потребность в материалах.
        /// </summary>
        /// <param name="werehouse"></param>
        /// <returns></returns>
        [HttpPost, Route("create-request")]
        public async Task<IActionResult> Create([FromBody] object request) {
            BaseRequest baseRequest = new RequestService(_db);
            await baseRequest.CreateRequest(request);

            return Ok("Заявка на потребность успешно создана");
        }      

        /// <summary>
        /// Метод сохраняет изменения в заявке.
        /// </summary>
        /// <returns></returns>
        [HttpPut, Route("save-change-request")]
        public async Task<IActionResult> SaveChangeRequest([FromBody] object request) {
            BaseRequest baseRequest = new RequestService(_db);
            await baseRequest.Edit(request);

            return Ok("Заявка успешно изменена");
        }

        /// <summary>
        /// Метод помечает заявку для удаления.
        /// </summary>
        [HttpPut, Route("post-delete-request")]
        public async Task<IActionResult> DeleteRequest([FromQuery] int number) {
            BaseRequest baseRequest = new RequestService(_db);
            await baseRequest.PostDeleteRequest(number);

            return Ok("Заявка переведена в статус для удаления");
        }

        /// <summary>
        /// Метод получает список новых заявок.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("get-new-requests")]
        public async Task<IActionResult> GetDataNewRequests() {
            BaseRequest template = new RequestService(_db);
            var oRequests = await template.GetDynamicDataNewRequests();

            return Ok(oRequests);
        }

        /// <summary>
        /// Метод получает список заявок в работе.
        /// </summary>
        [HttpGet, Route("get-inwork-requests")]
        public async Task<IActionResult> GetDataInWorkRequests() {
            BaseRequest template = new RequestService(_db);
            var oRequests = await template.GetDynamicDataWorkRequests();

            return Ok(oRequests);
        }

        /// <summary>
        /// Метод получает заявки, которые ожидают подтверждения удаления.
        /// </summary>
        [HttpGet, Route("get-accept-delete-requests")]
        public async Task<IActionResult> GetAcceptDeleteRequests() {
            BaseRequest template = new RequestService(_db);
            var oRequests = await template.GetDynamicDataAcceptDeleteRequests();

            return Ok(oRequests);
        }

        /// <summary>
        /// Метод получает материалы, которые нужно пополнить.
        /// </summary>
        [HttpPost, Route("get-refill-materials")]
        public async Task<IActionResult> GetRefillMaterials() {
            BaseRequest template = new RequestService(_db);
            var oMaterials = await template.GetDynamicDataRefillMaterials();

            return Ok(oMaterials);
        }

        /// <summary>
        /// Метод получает материалы, которые нужно сопоставить.
        /// </summary>
        [HttpPost, Route("get-mapping-materials")]
        public async Task<IActionResult> GetMappingMaterials() {
            BaseRequest template = new RequestService(_db);

            return Ok(await template.GetDynamicDataMappingMaterials());
        }

        /// <summary>
        /// Метод получает заявку для редактирования.
        /// </summary>
        [HttpGet, Route("get-request")]
        public async Task<IActionResult> GetEditRequest([FromQuery] int number) {
            BaseRequest baseRequest = new RequestService(_db);
            var res = await baseRequest.GetRequestForEdit(number);
            return Ok(res);
        }

        /// <summary>
        /// Метод изменяет статус на "В работе".
        /// </summary>
        [HttpGet, Route("change-status-inwork")]
        public async Task<IActionResult> ChangeRequestStatusInWork([FromQuery] int number) {
            BaseRequest baseRequest = new RequestService(_db);
            var oResult = await baseRequest.ChangeRequestStatusInWork(number);

            // Значит заявка уже в работе.
            if (oResult != null)
                return BadRequest(oResult);

            return Ok("Статус заявки успешно изменен");
        }
    }      
}

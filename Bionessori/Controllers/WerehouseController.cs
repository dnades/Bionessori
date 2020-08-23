using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core;
using Bionessori.Core.Data;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Bionessori.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionessori.Controllers {
    /// <summary>
    /// Контроллер описывает работу номенклатуры.
    /// </summary>
    [ApiController, Route("api/werehouse/material")]
    public class WerehouseController : ControllerBase {
        static ApplicationDbContext _db;
        public WerehouseController(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Метод получает список материалов со складов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-materials")]
        public async Task<IActionResult> GetMaterials() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);

            return Ok(await baseWerehouse.GetMaterials());
        }

        /// <summary>
        /// Метод получает список названий складов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-werehouses")]
        public async Task<IActionResult> GetNameWerehouses() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);

            return Ok(await baseWerehouse.GetNameWerehouses());
        }

        /// <summary>
        /// Метод получает список групп материалов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-groups")]
        public async Task<IActionResult> GetGroupshouses() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);

            return Ok(await baseWerehouse.GetGroupsWerehouses());
        }

        /// <summary>
        /// Метод получает список ед.изм.
        /// </summary>
        /// <returns>Список с единицами измерения.</returns>
        [HttpPost, Route("get-measures")]
        public async Task<IActionResult> GetMeasuresWerehouses() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);

            return Ok(await baseWerehouse.GetMeasuresWerehouses());
        }

        /// <summary>
        /// Метод получает список названий материалов без дубликатов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-distinct-materials")]
        public async Task<IActionResult> GetDistinctMaterials() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);

            return Ok(await baseWerehouse.GetDistinctMaterials());
        }

        /// <summary>
        /// Метод получает список материалов определенной группы.
        /// </summary>
        /// <param name="group"></param>
        /// <returns>Материалы группы.</returns>
        [HttpGet, Route("get-material-group")]
        public async Task<IActionResult> GetMaterialsGroup([FromQuery] string group) {
            //var oMaterialsGroup = await _werehouse.GetMaterialsGroup(group);

            return Ok();
        }

        /// <summary>
        /// Метод получает кол-во заявок со статусом "Новая".
        /// </summary>
        [HttpGet, Route("count-status-new")]
        public async Task<IActionResult> GetCountStatusNewRequests() {
            //var iRequests = await _werehouse.GetCountNewRequests();
            
            return Ok();
        }

        /// <summary>
        /// Метод получает кол-во заявок со статусом "В работе".
        /// </summary>
        [HttpGet, Route("count-status-work")]
        public async Task<IActionResult> GetCountStatusWorkRequests() {
            //var iRequests = await _werehouse.GetCountRequestInWork();

            return Ok();
        }

        /// <summary>
        /// Метод получает кол-во материалов, требующих пополнения.
        /// </summary>
        [HttpGet, Route("count-refill-materials")]
        public async Task<IActionResult> GetCountRefillMaterials() {
            //var iMaterials = await _werehouse.GetCountRefillMaterials();

            return Ok();
        }

        /// <summary>
        /// Метод получает кол-во материалов, требующих сопоставления.
        /// </summary>
        [HttpGet, Route("count-mapping-materials")]
        public async Task<IActionResult> GetCountMappingMaterials() {
            //var iMaterials = await _werehouse.GetCountMappingMaterials();

            return Ok();
        }

        /// <summary>
        /// Метод получает кол-во заявок, требующих подтверждения удаления.
        /// </summary>
        [HttpGet, Route("count-accept-delete-request")]
        public async Task<IActionResult> GetCountAcceptDeleteRequest() {
            //var iRequests = await _werehouse.GetCountAcceptDeleteRequests();
            
            return Ok();
        }        
    }
}

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
        ApplicationDbContext _db;
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
            var oMaterials = await baseWerehouse.GetMaterials();

            return Ok(oMaterials);
        }

        /// <summary>
        /// Метод получает список названий складов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-werehouses")]
        public async Task<IActionResult> GetNameWerehouses() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);
            var oNames = await baseWerehouse.GetNameWerehouses();

            return Ok(oNames);
        }

        /// <summary>
        /// Метод получает список групп материалов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-groups")]
        public async Task<IActionResult> GetGroupshouses() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);
            var oGroups = await baseWerehouse.GetGroupsWerehouses();

            return Ok(oGroups);
        }

        /// <summary>
        /// Метод получает список ед.изм.
        /// </summary>
        /// <returns>Список с единицами измерения.</returns>
        [HttpPost, Route("get-measures")]
        public async Task<IActionResult> GetMeasuresWerehouses() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);
            var oMeasures = await baseWerehouse.GetMeasuresWerehouses();

            return Ok(oMeasures);
        }

        /// <summary>
        /// Метод получает список названий материалов без дубликатов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-distinct-materials")]
        public async Task<IActionResult> GetDistinctMaterials() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);
            var oDistinctMaterials = await baseWerehouse.GetDistinctMaterials();

            return Ok(oDistinctMaterials);
        }

        /// <summary>
        /// Метод получает список материалов определенной группы.
        /// </summary>
        /// <param name="group"></param>
        /// <returns>Материалы группы.</returns>
        [HttpGet, Route("get-material-group")]
        public async Task<IActionResult> GetMaterialsGroup([FromQuery] string group) {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);
            var oMaterialGroups = await baseWerehouse.GetMaterialsGroup(group);

            return Ok(oMaterialGroups);
        }

        /// <summary>
        /// Метод получает кол-во заявок со статусом "Новая".
        /// </summary>
        [HttpGet, Route("count-status-new")]
        public async Task<IActionResult> GetCountStatusNewRequests() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);
            int countNewReqs = await baseWerehouse.GetCountNewRequests();

            return Ok(countNewReqs);
        }

        /// <summary>
        /// Метод получает кол-во заявок со статусом "В работе".
        /// </summary>
        [HttpGet, Route("count-status-work")]
        public async Task<IActionResult> GetCountStatusWorkRequests() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);
            int countInWrkReqs = await baseWerehouse.GetCountRequestInWork();

            return Ok(countInWrkReqs);
        }

        /// <summary>
        /// Метод получает кол-во материалов, требующих пополнения.
        /// </summary>
        [HttpGet, Route("count-refill-materials")]
        public async Task<IActionResult> GetCountRefillMaterials() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);
            int countRefillMat = await baseWerehouse.GetCountRefillMaterials();

            return Ok(countRefillMat);
        }

        /// <summary>
        /// Метод получает кол-во материалов, требующих сопоставления.
        /// </summary>
        [HttpGet, Route("count-mapping-materials")]
        public async Task<IActionResult> GetCountMappingMaterials() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);
            int countMappMaterials = await baseWerehouse.GetCountMappingMaterials();

            return Ok(countMappMaterials);
        }

        /// <summary>
        /// Метод получает кол-во заявок, требующих подтверждения удаления.
        /// </summary>
        [HttpGet, Route("count-accept-delete-request")]
        public async Task<IActionResult> GetCountAcceptDeleteRequest() {
            BaseWerehouse baseWerehouse = new WerehouseService(_db);
            int countAcceptDelReqs = await baseWerehouse.GetCountAcceptDeleteRequests();

            return Ok(countAcceptDelReqs);
        }        
    }
}

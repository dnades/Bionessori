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
    /// Контроллер описывает работу номенклатуры.
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
        /// Метод получает список названий складов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-werehouses")]
        public async Task<IActionResult> GetNameWerehouses() { 
            var oNames = await _werehouse.GetNameWerehouses();

            return Ok(oNames);
        }

        /// <summary>
        /// Метод получает список групп материалов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-groups")]
        public async Task<IActionResult> GetGroupshouses() {
            var oGroups = await _werehouse.GetGroupsWerehouses();

            return Ok(oGroups);
        }

        /// <summary>
        /// Метод получает список ед.изм.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-measures")]
        public async Task<IActionResult> GetMeasuresWerehouses() {
            var oMeasures = await _werehouse.GetMeasuresWerehouses();

            return Ok(oMeasures);
        }

        /// <summary>
        /// Метод получает список названий материалов без дубликатов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-distinct-materials")]
        public async Task<IActionResult> GetDistinctMaterials() {
            var oDistinctMaterials = await _werehouse.GetDistinctMaterials();

            return Ok(oDistinctMaterials);
        }

        /// <summary>
        /// Метод получает список материалов определенной группы.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpGet, Route("get-material-group")]
        public async Task<IActionResult> GetMaterialsGroup([FromQuery] string group) {
            var oMaterialsGroup = await _werehouse.GetMaterialsGroup(group);

            return Ok(oMaterialsGroup);
        }

        /// <summary>
        /// Метод получает кол-во заявок со статусом "Новая".
        /// </summary>
        [HttpGet, Route("count-status-new")]
        public async Task<IActionResult> GetCountStatusNewRequests() {
            var iRequests = await _werehouse.GetCountNewRequests();
            
            return Ok(iRequests);
        }

        /// <summary>
        /// Метод получает кол-во заявок со статусом "В работе".
        /// </summary>
        [HttpGet, Route("count-status-work")]
        public async Task<IActionResult> GetCountStatusWorkRequests() {
            var iRequests = await _werehouse.GetCountRequestInWork();

            return Ok(iRequests);
        }

        /// <summary>
        /// Метод получает кол-во материалов, требующих пополнения.
        /// </summary>
        [HttpGet, Route("count-refill-materials")]
        public async Task<IActionResult> GetCountRefillMaterials() {
            var iMaterials = await _werehouse.GetCountRefillMaterials();

            return Ok(iMaterials);
        }

        /// <summary>
        /// Метод получает кол-во материалов, требующих сопоставления.
        /// </summary>
        [HttpGet, Route("count-mapping-materials")]
        public async Task<IActionResult> GetCountMappingMaterials() {
            var iMaterials = await _werehouse.GetCountMappingMaterials();

            return Ok(iMaterials);
        }

        /// <summary>
        /// Метод получает кол-во заявок, требующих подтверждения удаления.
        /// </summary>
        [HttpGet, Route("count-accept-delete-request")]
        public async Task<IActionResult> GetCountAcceptDeleteRequest() {
            var iRequests = await _werehouse.GetCountAcceptDeleteRequests();
            
            return Ok(iRequests);
        }        
    }
}

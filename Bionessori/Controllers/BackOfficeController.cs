﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionessori.Controllers {
    /// <summary>
    /// Контроллер описывает работу back-офиса, в котором происходит все администрирование ERP-системы.
    /// </summary>
    [ApiController, Route("api/back-office")]
    public class BackOfficeController : ControllerBase {
        IBackOffice _office;

        public BackOfficeController(IBackOffice office) {
            _office = office;
        }

        /// <summary>
        /// Метод получает список пользователей.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-users")]
        public async Task<IActionResult> GetUsers() {
            var oUsers = await _office.GetUsers();

            return Ok(oUsers);
        }

        /// <summary>
        /// Метод назначает роль пользователю.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost, Route("give-role")]
        public async Task<IActionResult> GiveRole([FromBody] UserRole role) {
            await _office.GiveRole(role);

            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bionessori.Controllers {
    /// <summary>
    /// Контроллер описывает работу с личным кабинетом.
    /// </summary>
    [ApiController, Route("api/front-office")]
    public class FrontOfficeController : ControllerBase {
        IFrontOffice _office;

        public FrontOfficeController(IFrontOffice office) {
            _office = office;
        }

        /// <summary>
        /// Метод получает информацию сотрудника.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost, Route("get-employee-info")]
        public async Task<IActionResult> GetEmployeeInfo(Employee employee) {
            try {
                if (string.IsNullOrEmpty(employee.Login)) {
                    throw new ArgumentNullException();
                }

                // Получает данные сотрудника.
                var oEmployee = await _office.GetEmployeeInfo(employee.Login);

                return Ok(oEmployee);
            }
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException($"Логин не передан {ex.Message}");
            }
        }

        /// <summary>
        /// Метод получает список записей на прием к конкретному врачу.
        /// </summary>
        [HttpPost, Route("employee-receptions")]
        public async Task<IActionResult> GetEmployeeReceptions([FromBody] User user) {
            var oEmployeeReceptions = await _office.GetEmployeeReceptions(user.Login);

            return Ok(oEmployeeReceptions);
        }

        /// <summary>
        /// Метод добавляет новое расписание сотрудника.
        /// </summary>
        [HttpPost, Route("add-schedule")]
        public async Task<IActionResult> AddSchedule([FromBody] Schedule schedule) {
            await _office.AddSchedule(schedule);

            return Ok("Новое расписание успешно создано");
        }

        /// <summary>
        /// Метод удаляет записи на прием из ЛК.
        /// TODO: Доработать случай множественного удаления.
        /// </summary>
        [HttpPost, Route("delete-reception")]
        public async Task<IActionResult> DeleteReception(Reception reception) {
            await _office.DeleteReception(reception);

            return Ok("Удаление прошло успешно");
        }
    }
}

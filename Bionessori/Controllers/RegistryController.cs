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
        public async Task<IActionResult> IdentityPatient([FromBody] PatientCard patientCard) {
            var isPatient = await _registry.GetIdentityPatient(patientCard);

            return Ok(isPatient);
        }

        /// <summary>
        /// Метод получает список расписаний.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-schedules")]
        public async Task<IActionResult> GetSchedules([FromBody] Employee employee) {
            var oSchedules = await _registry.GetSchedules(employee.FullName);

            return Ok(oSchedules);
        }

        /// <summary>
        /// Метод получает ФИО и специализацию врача.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost, Route("get-partial-employee")]
        public async Task<IActionResult> GetPartialEmployee([FromBody] User user) {
            var oUser = await _registry.GetUserId(user.Login);
            var oEmp = new {
                fullName = oUser.FullName,
                position = oUser.Position
            };

            return Ok(oEmp);
        }

        /// <summary>
        /// Метод получает список сотрудников.
        /// </summary>
        [HttpPost, Route("get-employees")]
        public async Task<IActionResult> GetEmployees() {
            var oEmployees = await _registry.GetEmployees();

            return Ok(oEmployees);
        }

        /// <summary>
        /// Метод записывает пациента на прием.
        /// </summary>
        [HttpPost, Route("write-reception")]
        public async Task<IActionResult> WriteReception([FromBody] PatientCard patientCard) {
            await _registry.Write(patientCard);

            return Ok("Запись успешно создана");
        }

        /// <summary>
        /// Метод получает список записей на прием.
        /// </summary>
        [HttpPost, Route("get-receptions")]
        public async Task<IActionResult> GetReceptions() {
            var oReceptions = await _registry.GetReceptions();

            return Ok(oReceptions);
        }

        /// <summary>
        /// Метод редактирует запись пациента.
        /// </summary>
        [HttpPost, Route("edit-reception")]
        public async Task<IActionResult> EditReception(Reception reception) {
            await _registry.EditReception(reception);

            return Ok("Запись успешно изменена");
        }

        /// <summary>
        /// Метод удаляет запись.
        /// </summary>
        [HttpDelete, Route("delete-reception")]
        public async Task<IActionResult> DeleteReception(int id) {
            try {
                if (id == 0) {
                    throw new ArgumentException();
                }

                await _registry.DeleteReception(id);

                return Ok("Запись успешно удалена");
            }
            catch(ArgumentException ex) {
                throw new ArgumentException($"Id не передан {ex.Message}");
            }            
        }
    }
}

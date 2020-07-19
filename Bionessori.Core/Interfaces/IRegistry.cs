using Bionessori.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core.Interfaces {
    /// <summary>
    /// Интерфейс описывает работу регистратуры.
    /// </summary>
    public interface IRegistry {
        /// <summary>
        /// Метод записывает пациента на прием к врачу.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task Write(PatientCard patient);

        /// <summary>
        /// Метод вызывает скорую помощь пациенту.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task Call(PatientCard patient);

        /// <summary>
        /// Метод получает номера картпациентов.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task<List<string>> LoadCardsNumbers();

        /// <summary>
        /// Метод проверяет существование пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        Task<dynamic> GetIdentityPatient(PatientCard patientCard);

        /// <summary>
        /// Метод получает расписания врачей.
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetSchedules();

        /// <summary>
        /// Метод получает список врачей.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<List<Employee>> GetEmployees();

        /// <summary>
        /// Метод получает ФИО и специализацию конкретного врача.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<Employee> GetPartialEmployee(int id);

        /// <summary>
        /// Метод на вход принимает логин юзера, находит его в базе и выдает его id.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<Employee> GetUserId(User user);
    }
}

﻿using Bionessori.Models;
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
        /// Метод получает номера карт пациентов.
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
        Task<List<string>> GetSchedules(string fullName);

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
        Task<Employee> GetUserId(string login);

        /// <summary>
        /// Метод получает список всех записей на прием.
        /// </summary>
        /// <returns></returns>
        Task<List<Reception>> GetReceptions();

        /// <summary>
        /// Метод редактирует запись на прием.
        /// </summary>
        /// <param name="reception"></param>
        /// <returns></returns>
        Task EditReception(Reception reception);

        /// <summary>
        /// Метод удаляет запись на прием.
        /// </summary>
        /// <param name="reception"></param>
        /// <returns></returns>
        Task DeleteReception(int id);

        /// <summary>
        /// Метод находит конкретную запись на прием.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Reception> GetReception(int id);

        /// <summary>
        /// Метод получает названия мест направлений пациентов.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetSeatDirections();

        /// <summary>
        /// Метод получает имена пациентов.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetPatientNames();

        /// <summary>
        /// Метод создает направление.
        /// </summary>
        /// <returns></returns>
        Task CreateDirection(Direction direction);

        /// <summary>
        /// Метод редактирует направление.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task EditDirection(Direction direction);

        /// <summary>
        /// Метод удаляет направление.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteDirection(int id);

        /// <summary>
        /// Метод выбирает список направлений.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetDirections();

        /// <summary>
        /// Метод получает список типов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetDirectionsType();

        /// <summary>
        /// Метод получает список статусов направлений.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetDirectionsStatus();
    }
}

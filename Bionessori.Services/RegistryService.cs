﻿using Bionessori.Core.Extensions;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис реализует методы работы регистратуры.
    /// </summary>
    public class RegistryService : IRegistry {
        string _connectionString = null;

        public RegistryService(string conn) {
            _connectionString = conn;
        }

        public Task Call(PatientCard patient) {
            throw new NotImplementedException();
        }
       
        /// <summary>
        /// Метод получает номера карт пациентов.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> LoadCardsNumbers() {
            var parameters = new DynamicParameters();
            string sParam = CardExtension.Number.ToString();
            parameters.Add("@param", sParam, DbType.String);

            using (var db = new SqlConnection(_connectionString)) {
                // Процедура получает список номеров карт пациентов.
                var oNumbersCards = await db.QueryAsync<string>("dbo.sp_GetAllCards",
                    commandType: CommandType.StoredProcedure,
                    param: parameters);

                return oNumbersCards.ToList();
            }
        }

        /// <summary>
        /// Метод записывает на пациента прием.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task Write(PatientCard patient) {
            int sNum = Convert.ToInt32(RandomDataService.GenerateRandomNumber());    // Получает рандомный номер записи на прием.
            int userId = await GetUserIds(patient.FullName);

            try {
                using (var db = new SqlConnection(_connectionString)) {
                    var oReception = await db.QueryAsync($"INSERT INTO dbo.Receptions (date, number_reception, employee_id, card_number) " +
                        $"VALUES ('{patient.TimeProcRecommend}', {sNum}, {userId}, '{patient.CardNumber}')");
                }                
            }
            catch(Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Получает id сотрудника по его имени.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<int> GetUserIds(string login) {
            using (var db = new SqlConnection(_connectionString)) {
                var result = await db.QueryAsync<string>($"SELECT id FROM dbo.Employees WHERE full_name = '{login}'");
                int userId = Convert.ToInt32(result.FirstOrDefault());

                return userId;
            }
        }

        /// <summary>
        /// Метод проверяет существование пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        public async Task<dynamic> GetIdentityPatient(PatientCard patientCard) {
            try {
                // Проверяет, существует ли пациент.
                using (var db = new SqlConnection(_connectionString)) {
                    var oPatient = await db.QueryAsync($"" +
                        $"SELECT card_number AS cardNumber " +
                        $"FROM dbo.PatientCards " +
                        $"WHERE date_of_birth = '{patientCard.DateOfBirth}' " +
                        $"AND policy = '{patientCard.Policy}'");

                    bool isPatient = Convert.ToBoolean(oPatient.Count());

                    if (!isPatient) {
                        throw new ArgumentNullException("Такого пациента не существует");
                    }

                    return oPatient;
                }
            }
            catch(Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает расписания врачей.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetSchedules(string fullName) {
            var parameters = new DynamicParameters();
            parameters.Add("@fullName", fullName, DbType.String);

            using (var db = new SqlConnection(_connectionString)) {
                // Процедура получает список расписаний врача.
                var oSchedules = await db.QueryAsync<string>("dbo.sp_GetSchedules",
                    commandType: CommandType.StoredProcedure,
                    param: parameters);

                return oSchedules.ToList();
            }   
        }

        /// <summary>
        /// Метод получает список врачей.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<List<Employee>> GetEmployees() {
            using (var db = new SqlConnection(_connectionString)) {
                var oEmployees = await db.QueryAsync<Employee>("dbo.sp_GetEmployees");

                return oEmployees.ToList();
            }
        }

        /// <summary>
        /// Метод получает ФИО и специализацию конкретного врача.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<Employee> GetPartialEmployee(int id) {
            var parameters = new DynamicParameters();
            parameters.Add("@userId", id, DbType.Int32);

            using (var db = new SqlConnection(_connectionString)) {
                // Продедура получает ФИО и специализацию конкретного врача.
                var oPartialEmployee = await db.QueryAsync<Employee>("dbo.sp_GetConcreteEmployee",
                    commandType: CommandType.StoredProcedure,
                    param: parameters);

                return oPartialEmployee.FirstOrDefault();
            }
        }

        /// <summary>
        /// Метод полуает id юзера по его логину.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<Employee> GetUserId(string login) {
            using (var db = new SqlConnection(_connectionString)) {
                var oUser = await db.QueryAsync<Employee>($"SELECT id FROM u0772479_admin.Users WHERE login = '{login}'");
                int id = oUser.FirstOrDefault().Id;

                // Получает данные врача.
                var oEmployee = await GetPartialEmployee(id);

                return oEmployee;
            }
        }

        /// <summary>
        /// Метод реализует получение списка записей на прием.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Reception>> GetReceptions() {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    // Процедура получает список записей на прием к врачу.
                    var oReceptions = await db.QueryAsync<Reception>("dbo.sp_GetReceptions");

                    return oReceptions.ToList();
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод реализует редактирование записи на прием.
        /// </summary>
        /// <param name="reception"></param>
        /// <returns></returns>
        public async Task EditReception(Reception reception) {
            try {
                if (string.IsNullOrEmpty(reception.Date)) {
                    throw new ArgumentNullException();
                }

                using (var db = new SqlConnection(_connectionString)) {
                    // Обновляет данные записи на прием.
                    await db.QueryAsync($"UPDATE dbo.Receptions SET date = '{reception.Date}' WHERE id = {reception.Id}");
                }
            }            
            catch(ArgumentNullException ex) {
                throw new ArgumentNullException("Входные параметры не заполнены", ex.Message.ToString());
            }
            catch (Exception ex) {
                throw new Exception($"Произошла неизвестная ошибка {ex.Message}");
            }
        }

        /// <summary>
        /// Метод реализует удаление записи на прием.
        /// </summary>
        /// <param name="reception"></param>
        /// <returns></returns>
        public async Task DeleteReception(int id) {           
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    // Удаляет запись на прием.
                    await db.QueryAsync($"DELETE dbo.Receptions WHERE id = {id}");
                }
            }
            catch(Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод находит конкретную запись на прием.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Reception> GetReception(int id) {
            try {
                if (Convert.ToInt32(id) == 0) {
                    throw new ArgumentNullException();
                }

                using (var db = new SqlConnection(_connectionString)) {
                    var oReception = await db.QueryAsync<Reception>($"SELECT * FROM dbo.Receptions WHERE id = {id}");

                    return oReception.FirstOrDefault();
                }
            }
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException("Id не передан", ex.Message.ToString());
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выбирает название мест направлений пациентов.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetSeatDirections() {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    var oDirectNames = await db.QueryAsync<string>("SELECT name_direction FROM dbo.SeatDirections");

                    return oDirectNames;
                }
            }
            catch (Exception ex) {
                throw new ArgumentException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает имена пациентов.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetPatientNames() {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    var oNames = await db.QueryAsync<string>("SELECT full_name AS fullName FROM dbo.PatientCards");

                    return oNames;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод создает направление.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public async Task CreateDirection(Direction direction) {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    // Получает id карты пациента по его имени.
                    var objCard = await db.QueryAsync<string>($"SELECT card_number FROM dbo.PatientCards " +
                        $"WHERE full_name = '{direction.PatientName}'");

                    // Получает id места направления по его названию.
                    var objDirect = await db.QueryAsync<int>($"SELECT id FROM dbo.SeatDirections " +
                        $"WHERE name_direction = '{direction.SeatDirection}'");

                    // Находит Id сотрудника по его имени.
                    int employeeId = await GetUserIds(direction.EmployeeName);

                    // Генерит номер направления.
                    int RandomGenerate() {
                        return RandomDataService.GenerateRandomNumber();
                    }
                    int generateNumberDirect = RandomGenerate();

                    // Сохраняет новое направление.
                    await db.QueryAsync($"INSERT INTO dbo.Directions (card_number, number_direction, seat_direction_id, direction_type, direction_status, employee_id) " +
                        $"VALUES ({objCard.FirstOrDefault()}, {generateNumberDirect}, {objDirect.FirstOrDefault()}, '{direction.Type}', '{direction.Status}', {employeeId})");
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод редактирует направление.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>Статус редактирования.</returns>
        public async Task EditDirection(Direction direction) {
            try {                
                using (var db = new SqlConnection(_connectionString)) {
                    // Получает id карты пациента по его имени.
                    var objCard = await db.QueryAsync<string>($"SELECT card_number FROM dbo.PatientCards " +
                        $"WHERE full_name = '{direction.PatientName}'");

                    // Получает id места направления по его названию.
                    var objDirect = await db.QueryAsync<int>($"SELECT id FROM dbo.SeatDirections " +
                        $"WHERE name_direction = '{direction.SeatDirection}'");

                    // Находит Id сотрудника по его имени.
                    int employeeId = await GetUserIds(direction.EmployeeName);
                    
                    // Редактирует направление.
                    await db.QueryAsync($"UPDATE dbo.Directions SET " +
                        $"card_number = '{objCard.FirstOrDefault()}'," +
                        $"seat_direction_id = '{objDirect.FirstOrDefault()}'," +
                        $"direction_type = '{direction.Type}'," +
                        $"direction_status = '{direction.Status}'," +
                        $"employee_id = {employeeId} " +
                        $"WHERE id = {direction.Id}");
                }
            }
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException("Входные параметры не заполнены", ex);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод удаляет направление.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteDirection(int id) {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    // Удаляет направление.
                    await db.QueryAsync($"DELETE dbo.Directions WHERE id = {id}");
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выбирает список направлений.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> GetDirections() {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    var oDirections = await db.QueryAsync("dbo.sp_GetDirections");

                    return oDirections;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает типы направлений.
        /// </summary>
        /// <returns>Список с типами.</returns>
        public async Task<IEnumerable<string>> GetDirectionsType() {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    var oTypes = await db.QueryAsync<string>("sp_GetDirectionsType");

                    return oTypes;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает статусы направлений.
        /// </summary>
        /// <returns>Список статусов.</returns>
        public async Task<IEnumerable<string>> GetDirectionsStatus() {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    var oStatuses = await db.QueryAsync<string>("sp_GetDirectionsStatus");

                    return oStatuses;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}

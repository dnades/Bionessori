using Bionessori.Core.Extensions;
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
    ///  Сервис описывает работу личного кабинета.
    /// </summary>
    public class FrontOfficeService : IFrontOffice {
        string _connectionString;

        public FrontOfficeService(string connectionString) {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Метод получает информацию сотрудника.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetEmployeeInfo(string userName) {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    var oEmployee = await db.QueryAsync<Employee>($"SELECT t1.id, t1.full_name AS fullName, t1.position, t1.address, t1.post_code AS postCode, t1.contact_number AS contactNumber, t1.tab_number AS tabNumber, t1.date_birth AS dateBirth, t1.start_date_work AS startDateWork, t1.number_passport AS numberPassport, t1.age, t1.number_seat_work AS numberSeatWork, t1.department_id AS departmentId, t1.user_id AS userId, t2.login " +
                        $"FROM dbo.Employees t1 " +
                        $"JOIN u0772479_admin.Users t2 ON t1.user_id = t2.id WHERE t2.login = '{userName}'");

                    return oEmployee;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод реализует получение записей сотрудника.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Reception>> GetEmployeeReceptions(string fullName) {
            try {
                if (string.IsNullOrEmpty(fullName)) {
                    throw new ArgumentNullException();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@userName", fullName, DbType.String);

                using (var db = new SqlConnection(_connectionString)) {
                    var oEmployeeReceptions = await db.QueryAsync<Reception>("dbo.sp_GetEmployeeReceptions",
                        commandType: CommandType.StoredProcedure,
                        param: parameters);

                    return oEmployeeReceptions.AsEnumerable();
                }
            }
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException("Логин не передан", ex.Message.ToString());
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод реализует добавление информации сотрудника.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task AddEmployeeInfo(Employee employee) {
            string seatType = "";    // Тип места работы сотрудника.

           try {
                using (var db = new SqlConnection(_connectionString)) {
                    // Записывает тип места работы сотрудника в таблицу сотрудников (поликлиника или стационар).
                    switch (employee.SeatType) {
                        case "Поликлиника":
                            seatType = SeatTypeExtension.Поликлиника.ToString();
                            break;

                        case "Стационар":
                            seatType = SeatTypeExtension.Стационар.ToString();
                            break;
                    }

                    // Добавляет информацию о сотруднике.
                    await db.QueryAsync($"INSERT INTO dbo.Employees (full_name, position, address, post_code, contact_number, tab_number, date_birth, number_passport, age, number_seat_work, seat_type) VALUES ('{employee.FullName}', '{employee.Position}', '{employee.Address}', '{employee.PostCode}', '{employee.Number}', '{employee.TabNumber}', '{employee.DateBirth}', '{employee.PasportNumber}', {employee.Age}, '{employee.NumberSeatWork}', '{seatType}')");
                }
            }
            catch (Exception ex) {
                throw new Exception("Неизвестная ошибка", ex);
            }
        }
    }
}

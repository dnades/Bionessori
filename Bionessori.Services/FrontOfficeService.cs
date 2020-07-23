using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                    var oEmployee = await db.QueryAsync<Employee>($"SELECT t1.id, t1.full_name AS fullName, t1.position, t1.address, t1.post_code AS postCode, t1.contact_number AS contactNumber, t1.tab_number AS tabNumber, t1.date_birth AS dateBirth, t1.start_date_work AS startDateWork, t1.number_passport AS numberPassport, t1.age, t1.number_seat_work AS numberSeatWork, t1.department_id AS departmentId, t1.user_id AS userId, t2.login FROM dbo.Employees t1 JOIN u0772479_admin.Users t2 ON t1.user_id = t2.id WHERE t2.login = '{userName}'");

                    return oEmployee;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}

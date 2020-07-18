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

        public Task Write(PatientCard patient) {
            throw new NotImplementedException();
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

                    //return Convert.ToBoolean(Convert.ToInt32(isPatient.ToList()[0]));
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
    }
}

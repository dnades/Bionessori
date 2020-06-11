using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис реализует методы работы с картами пациентов.
    /// </summary>
    public class CardPatientService : ICard {
        string _conStr = null;

        public CardPatientService(string conn) {
            _conStr = conn;
        }

        /// <summary>
        /// Метод получает список карт пациентов.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task<List<PatientCard>> Take() {
            using (var db = new SqlConnection(_conStr)) {
                var oCards = await db.QueryAsync<PatientCard>("sp_GetAllCards", commandType: CommandType.StoredProcedure);

                return oCards.ToList();
            }            
        }
    }
}

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
        /// Метод удаляет карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        public async Task<string> Delete(PatientCard patientCard) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод редактирует карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        public async Task<string> Edit(PatientCard patientCard) {
            return "Карта пациента успешно изменена.";
        }

        /// <summary>
        /// Метод получает список карт пациентов.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task<List<PatientCard>> Take() {
            using (var db = new SqlConnection(_conStr)) {
                // Вызывает процедуру для получения списка карт пациентов.
                var oCards = await db.QueryAsync<PatientCard>("sp_GetAllCards", 
                    commandType: CommandType.StoredProcedure);

                return oCards.ToList();
            }            
        }
    }
}

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
        /// Метод создает новую карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        public async Task<string> Create(PatientCard patientCard) {
            using (var db = new SqlConnection(_conStr)) {
                var parameters = new DynamicParameters();
                parameters.Add("@cardNumber", patientCard.CardNumber, DbType.Int32);
                parameters.Add("@fullName", patientCard.FullName, DbType.String);
                parameters.Add("@dateOfBirth", patientCard.DateOfBirth, DbType.DateTime);
                parameters.Add("@address", patientCard.Address, DbType.String);
                parameters.Add("@number", patientCard.Number, DbType.String);
                parameters.Add("@policy", patientCard.Policy, DbType.String);
                parameters.Add("@snails", patientCard.Snails, DbType.String);
                parameters.Add("@timeProcAndRec", patientCard.TimeProcRecommend, DbType.DateTime);
                parameters.Add("@prescriptionDrugs", patientCard.PrescriptionDrugs, DbType.String);
                parameters.Add("@diagnosis", patientCard.Diagnosis, DbType.String);
                parameters.Add("@recipesRecommend", patientCard.RecipesRecommend, DbType.String);
                parameters.Add("@medicalHistory", patientCard.MedicalHistory, DbType.String);
                parameters.Add("@doctor", patientCard.Doctor, DbType.String);

                // Вызывает процедуру создания новой карты пациента.
                await db.QueryAsync<PatientCard>("sp_CreateCard",
                    commandType: CommandType.StoredProcedure,
                    param: parameters);
            }

            return "Новая карта пациента успешно создана.";
        }

        /// <summary>
        /// Метод удаляет карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        public async Task<string> Delete(PatientCard patientCard) {
            using (var db = new SqlConnection(_conStr)) {
                await db.QueryAsync<PatientCard>($"DELETE FROM PatientCards WHERE id = {patientCard.Id}");
            }

            return "Карта пациента успешно удалена.";
        }

        /// <summary>
        /// Метод редактирует карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        public async Task<string> Edit(PatientCard patientCard) {
            using (var db = new SqlConnection(_conStr)) {
                var parameters = new DynamicParameters();
                parameters.Add("@id", patientCard.Id, DbType.Int32);
                parameters.Add("@cardNumber", patientCard.CardNumber, DbType.Int32);
                parameters.Add("@fullName", patientCard.FullName, DbType.String);
                parameters.Add("@dateOfBirth", patientCard.DateOfBirth, DbType.DateTime);
                parameters.Add("@address", patientCard.Address, DbType.String);
                parameters.Add("@number", patientCard.Number, DbType.String);
                parameters.Add("@policy", patientCard.Policy, DbType.String);
                parameters.Add("@snails", patientCard.Snails, DbType.String);
                parameters.Add("@timeProcAndRec", patientCard.TimeProcRecommend, DbType.DateTime);
                parameters.Add("@prescriptionDrugs", patientCard.PrescriptionDrugs, DbType.String);
                parameters.Add("@diagnosis", patientCard.Diagnosis, DbType.String);
                parameters.Add("@recipesRecommend", patientCard.RecipesRecommend, DbType.String);
                parameters.Add("@medicalHistory", patientCard.MedicalHistory, DbType.String);
                parameters.Add("@doctor", patientCard.Doctor, DbType.String);

                // Вызывает процедуру изменения карты пациента.
                await db.QueryAsync<PatientCard>("sp_UpdateCard",
                    commandType: CommandType.StoredProcedure,
                    param: parameters);
            }

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

﻿using Bionessori.Core.Interfaces;
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
        public async Task Create(PatientCard patientCard) {
            string typeParam = "card";
            int generateNumber;

            // Генерит рандомный номер заявки.
            int RandomGenerate() {
                return RandomDataService.GenerateRandomNumber();
            }

            generateNumber = RandomGenerate();

            // Проверяет существует ли уже такая карта.
            var resultCheck = await CheckingCard(typeParam, patientCard.CardNumber);

            // Если такая карта уже существует, то повторно пойдет генерить номер карты.
            if (Convert.ToBoolean(resultCheck)) {
                generateNumber = RandomGenerate();
            }

            patientCard.CardNumber = generateNumber;

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
                parameters.Add("@blood_group", patientCard.BloodGroup, DbType.String);
                parameters.Add("@category", patientCard.Category, DbType.String);
                parameters.Add("@seat_work", patientCard.SeatWork, DbType.String);
                parameters.Add("@position", patientCard.Position, DbType.String);
                parameters.Add("@tab_number", patientCard.TabNum, DbType.String);
                parameters.Add("@insurance_company", patientCard.InsuranceCompany, DbType.String);
                parameters.Add("@date_to", patientCard.DateTo, DbType.Date);
                parameters.Add("@comment", patientCard.Comment, DbType.String);
                parameters.Add("@email", patientCard.Email, DbType.String);
                parameters.Add("@indicator", patientCard.Indicator, DbType.String);
                parameters.Add("@isVich", patientCard.isVich, DbType.String);
                parameters.Add("@isHb", patientCard.isHb, DbType.String);
                parameters.Add("@isRw", patientCard.isRw, DbType.String);
                parameters.Add("@city", patientCard.City, DbType.String);
                parameters.Add("@region", patientCard.Region, DbType.String);
                parameters.Add("@district", patientCard.District, DbType.String);
                parameters.Add("@form_payment", patientCard.FormPay, DbType.String);
                parameters.Add("@plan_payment", patientCard.PlanPay, DbType.String);
                parameters.Add("@registry", patientCard.Registry, DbType.Date);
                parameters.Add("@who_change", patientCard.WhoChange, DbType.String);
                parameters.Add("@operator", patientCard.Operator, DbType.String);
                parameters.Add("@index_number", patientCard.IndexNumber, DbType.String);

                // Вызывает процедуру создания новой карты пациента.
                await db.QueryAsync<PatientCard>("sp_CreateCard",
                    commandType: CommandType.StoredProcedure,
                    param: parameters);
            }
        }

        /// <summary>
        /// Метод удаляет карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        public async Task Delete(int id) {
            try {
                if (id == 0) {
                    throw new ArgumentNullException();
                }

                // Удаляет карту пациента.
                using (var db = new SqlConnection(_conStr)) {
                    await db.QueryAsync<PatientCard>($"DELETE FROM dbo.PatientCards WHERE id = {id}");
                }
            }
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException("Id заявки не передан", ex.Message);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод редактирует карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        public async Task Edit(PatientCard patientCard) {
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
                parameters.Add("@blood_group", patientCard.BloodGroup, DbType.String);
                parameters.Add("@category", patientCard.Category, DbType.String);
                parameters.Add("@seat_work", patientCard.SeatWork, DbType.String);
                parameters.Add("@position", patientCard.Position, DbType.String);
                parameters.Add("@tab_number", patientCard.TabNum, DbType.String);
                parameters.Add("@insurance_company", patientCard.InsuranceCompany, DbType.String);
                parameters.Add("@date_to", patientCard.DateTo, DbType.Date);
                parameters.Add("@comment", patientCard.Comment, DbType.String);
                parameters.Add("@email", patientCard.Email, DbType.String);
                parameters.Add("@indicator", patientCard.Indicator, DbType.String);
                parameters.Add("@isVich", patientCard.isVich, DbType.String);
                parameters.Add("@isHb", patientCard.isHb, DbType.String);
                parameters.Add("@isRw", patientCard.isRw, DbType.String);
                parameters.Add("@city", patientCard.City, DbType.String);
                parameters.Add("@region", patientCard.Region, DbType.String);
                parameters.Add("@district", patientCard.District, DbType.String);
                parameters.Add("@form_payment", patientCard.FormPay, DbType.String);
                parameters.Add("@plan_payment", patientCard.PlanPay, DbType.String);
                parameters.Add("@registry", patientCard.Registry, DbType.Date);
                parameters.Add("@who_change", patientCard.WhoChange, DbType.String);
                parameters.Add("@operator", patientCard.Operator, DbType.String);
                parameters.Add("@index_number", patientCard.IndexNumber, DbType.String);

                // Вызывает процедуру изменения карты пациента.
                await db.QueryAsync<PatientCard>("sp_UpdateCard",
                    commandType: CommandType.StoredProcedure,
                    param: parameters);
            }
        }

        /// <summary>
        /// Метод получает список карт пациентов.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PatientCard>> Take() {            
            try {
                var parameters = new DynamicParameters();
                parameters.Add("@param", "", DbType.String);

                using (var db = new SqlConnection(_conStr)) {
                    // Вызывает процедуру для получения списка карт пациентов.
                    var oCards = await db.QueryAsync<PatientCard>("sp_GetAllCards",
                        commandType: CommandType.StoredProcedure,
                        param: parameters);

                    return oCards;
                }
            }
            catch(Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод проверяет существование карты пациента.
        /// </summary>
        /// <param name="typeParam"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> CheckingCard(string typeParam, int param) {
            using (var db = new SqlConnection(_conStr)) {
                var parameters = new DynamicParameters();
                parameters.Add("@type_param", typeParam, DbType.String);
                parameters.Add("@param", param, DbType.String);

                var checkCard = await db.QueryAsync<string>("dbo.sp_Checking",
                    commandType: CommandType.StoredProcedure,
                    param: parameters);

                return checkCard.ToArray()[0];
            }
        }

        /// <summary>
        /// Метод получает карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        public async Task<PatientCard> GetCard(PatientCard patientCard) {
            using (var db = new SqlConnection(_conStr)) {
                var oCard = await db.QueryAsync<PatientCard>($"SELECT * FROM PatientCards WHERE card_number = '{patientCard.CardNumber}'");

                return oCard.FirstOrDefault();
            }
        }
    }
}

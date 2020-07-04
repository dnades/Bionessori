using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Bionessori.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Tests {
    [TestClass]
    public class CardTests : ICard {
		string _conn = null;

		public CardTests() { }

		public CardTests(string conn) {
			_conn = conn;
		}

        public Task<string> Create(PatientCard patientCard) {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateCardTest() {
			//var strConn = _config.GetConnectionString("connectionString");
			
			Console.WriteLine();
			var testObjectCard = new PatientCard {
				CardNumber = "123",
				FullName = "Test",
				DateOfBirth = Convert.ToDateTime("1990-03-12"),
				Address = "test",
				Number = "test",
				Email = "test@mail.ru",
				Plan = "test",
				BloodGroup = "III",
				Policy = "test",
				Snails = "test",
				TimeProcRecommend = Convert.ToDateTime("2019-01-06T17:16:40"),
				PrescriptionDrugs = "test",
				Diagnosis = "test",
				RecipesRecommend = "test",
				MedicalHistory = "test",
				Doctor = "test",
				Category = "test",
				SeatWork = "test",
				Position = "test",
				TabNum = "test",
				InsuranceCompany = "test",
				DateTo = Convert.ToDateTime("2019-01-06T17:16:40"),
				Comment = "test",
				Indicator = "test",
				isVich = "false",
				isHb = "false",
				isRw = "false",
				City = "test",
				DopAddress = "test",
				Dop = "test",
				District = "test",
				Region = "test",
				FormPay = "test",
				Registry = Convert.ToDateTime("2019-01-06T17:16:40"),
				WhoChange = "test",
				Operator = "test"
			};

			using (var db = new SqlConnection(_conn)) {
				Console.WriteLine();
			}
			//var testCreateCard = new CardPatientService(strConn);
			//var result = testCreateCard.Create(testObjectCard);

			//Assert.IsTrue(result.Result);
			Console.WriteLine();
		}

        public Task<string> Delete(PatientCard patientCard) {
            throw new NotImplementedException();
        }

        public Task<string> Edit(PatientCard patientCard) {
            throw new NotImplementedException();
        }

        public Task<List<PatientCard>> Take() {
            throw new NotImplementedException();
        }
    }
}
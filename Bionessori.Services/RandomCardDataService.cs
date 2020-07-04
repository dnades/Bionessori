using Bionessori.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис генерации рандомных данных для карт пациентов.
    /// </summary>
    class RandomCardDataService : IRandom {
        /// <summary>
        /// Метод реализует генерацию рандомного номера карты пациента.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GenerateCardNumber() {
            Random r = new Random();
            string numberReq = r.Next(1, 99999).ToString();

            return numberReq;
        }
    }
}

using Bionessori.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис для генерации рандомных данных.
    /// </summary>
    public class RandomDataService {      
        /// <summary>
        /// Метод реализует генерацию рандомного номера карты пациента.
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomNumber() {
            Random r = new Random();
            string numberReq = r.Next(1, 99999).ToString();

            return numberReq;
        }
    }
}

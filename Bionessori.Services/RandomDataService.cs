using Bionessori.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис для генерации рандомных данных.
    /// </summary>
    public sealed class RandomDataService {      
        /// <summary>
        /// Метод реализует генерацию рандомного номера.
        /// </summary>
        /// <returns></returns>
        public static int GenerateRandomNumber() {
            Random r = new Random();
            int numberReq = r.Next(1, 99999);

            return numberReq;
        }
    }
}

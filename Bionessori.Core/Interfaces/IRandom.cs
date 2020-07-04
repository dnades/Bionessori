using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core.Interfaces {
    /// <summary>
    /// Интерфейс описывает методы генерации рандомных данных.
    /// </summary>
    public interface IRandom {
        /// <summary>
        /// Метод генерит рандомный номер карты пациента.
        /// </summary>
        /// <returns></returns>
        Task<string> GenerateCardNumber();
    }
}

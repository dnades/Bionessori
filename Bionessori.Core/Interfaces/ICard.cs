using Bionessori.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core.Interfaces {
    /// <summary>
    /// Интерфейс описывает методы работы с картами пациентов.
    /// </summary>
    public interface ICard {
        /// <summary>
        /// Метод получает список карт пациентов.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task<List<PatientCard>> Take();
    }
}

using Bionessori.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core.Interfaces {
    /// <summary>
    /// Интерфейс описывает работу регистратуры.
    /// </summary>
    public interface IRegistry {
        /// <summary>
        /// Метод записывает пациента на прием к врачу.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task Write(PatientCard patient);

        /// <summary>
        /// Метод вызывает скорую помощь пациенту.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task Call(PatientCard patient);

        /// <summary>
        /// Метод получает номера картпациентов.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task<List<string>> LoadCardsNumbers();
    }
}

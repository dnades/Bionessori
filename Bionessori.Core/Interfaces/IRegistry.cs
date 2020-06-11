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
        Task<PatientCard> Write(PatientCard patient);

        /// <summary>
        /// Метод вызывает скорую помощь пациенту.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task<PatientCard> Call(PatientCard patient);

        /// <summary>
        /// Метод отправляет карту пациента.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task<PatientCard> Send(PatientCard patient);
    }
}

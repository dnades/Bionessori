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

        /// <summary>
        /// Метод редактирует карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        Task<string> Edit(PatientCard patientCard);

        /// <summary>
        /// Метод удаляет карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        Task<string> Delete(PatientCard patientCard);

        /// <summary>
        /// Метод создаетновую карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        Task<string> Create(PatientCard patientCard);
    }
}

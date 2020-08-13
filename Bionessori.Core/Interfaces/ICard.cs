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
        Task<IEnumerable<PatientCard>> Take();

        /// <summary>
        /// Метод редактирует карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        Task Edit(PatientCard patientCard);

        /// <summary>
        /// Метод удаляет карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// Метод создаетновую карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        Task Create(PatientCard patientCard);

        /// <summary>
        /// Метод проверяет существование карты пациента.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<string> CheckingCard(string typeParam, int param);

        /// <summary>
        /// Метод получает конкретную карту пациента.
        /// </summary>
        /// <param name="patientCard"></param>
        /// <returns></returns>
        Task<PatientCard> GetCard(PatientCard patientCard);
    }
}

﻿using Bionessori.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core.Interfaces {
    /// <summary>
    /// Интерфейс описывает личный кабинет сотрудника.
    /// </summary>
    public interface IFrontOffice {
        /// <summary>
        /// Метод получает личную информацию сотрудника.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetEmployeeInfo(string userName);

        /// <summary>
        /// Метод получает список записей на прием к конкретному врачу.
        /// </summary>
        /// <returns>Коллекция записей.</returns>
        Task<IEnumerable<Reception>> GetEmployeeReceptions(string fullName);
    }
}
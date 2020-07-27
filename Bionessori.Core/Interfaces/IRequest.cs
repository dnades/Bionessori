using Bionessori.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core.Interfaces {
    /// <summary>
    /// Интерфейс описывает методы работы с заявками потребностей.
    /// </summary>
    public interface IRequest {
        /// <summary>
        /// Метод получает список заявок.
        /// </summary>
        /// <returns></returns>
        Task<object> GetRequests();

        /// <summary>
        /// Метод создает новую заявку на потребности в закупках.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Create(Request werehouse);

        /// <summary>
        /// Метод удаляет заявку на потребности в закупках.
        /// </summary>
        /// <returns></returns>
        Task Delete(string number);

        /// <summary>
        /// Метод изменяет существующую заявку на потребности в закупках.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Edit(Request request);
    }
}

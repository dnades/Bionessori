using Bionessori.Models;
using System;
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
        Task<List<Request>> GetRequests(Request request);

        /// <summary>
        /// Метод создает новую заявку на потребности в закупках.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> Create(Request werehouse);

        /// <summary>
        /// Метод удаляет заявку на потребности в закупках.
        /// </summary>
        /// <returns></returns>
        Task<string> Delete();

        /// <summary>
        /// Метод изменяет существующую заявку на потребности в закупках.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Request> Edit(Request request);
    }
}

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
    }
}

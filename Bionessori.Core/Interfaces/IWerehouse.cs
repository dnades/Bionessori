using Bionessori.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core.Interfaces {
    /// <summary>
    /// Интерфейс описывает работу складов.
    /// </summary>
    public interface IWerehouse {
        /// <summary>
        /// Метод получает список продуктов (под продуктом понимается не только пищевой товар).
        /// </summary>
        /// <returns></returns>
        Task<object> GetProducts(); 
    }
}

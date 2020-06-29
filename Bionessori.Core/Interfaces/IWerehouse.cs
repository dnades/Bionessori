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
        /// Метод получает список материалов.
        /// </summary>
        /// <returns></returns>
        Task<List<Werehouse>> GetMaterials(); 
    }
}

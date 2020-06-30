using Bionessori.Models;
using System;
using System.Collections;
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

        /// <summary>
        /// Метод получает названия складов.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable> GetNameWerehouses();

        /// <summary>
        /// Метод получает список групп материалов.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable> GetGroupsWerehouses();

        /// <summary>
        /// Метод получает список ед.измерений.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable> GetMeasuresWerehouses();

        /// <summary>
        /// Метод получает названия материалов без дубликатов.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable> GetDistinctMaterials();
    }
}

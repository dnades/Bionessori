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

        /// <summary>
        /// Метод выбирает все материалы группы.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetMaterialsGroup(string group);

        /// <summary>
        /// Метод получает кол-во заявок со статусом "Новая".
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        Task<int> GetCountNewRequests();

        /// <summary>
        /// Метод получает кол-во заявок со статусом "В работе".
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountRequestInWork();

        /// <summary>
        /// Метод получает кол-во материалов, которые нуждаются в пополнении.
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountRefillMaterials();

        /// <summary>
        /// Метод получает кол-во материалов, которые нуждаются в сопоставлении.
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountMappingMaterials();

        /// <summary>
        /// Метод получает кол-во заявок, требующих подтверждения удаления.
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAcceptDeleteRequests();
    }
}

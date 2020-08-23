using Bionessori.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core {
    public abstract class BaseRequest {
        /// <summary>
        /// Метод создает новую заявку.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract Task CreateRequest(object request);

        /// <summary>
        /// Метод получает список всех заявок.
        /// </summary>
        /// <returns>Список с заявками.</returns>
        public abstract Task<IEnumerable> GetRequests();

        /// <summary>
        /// Метод получает список заявок со статусом "Новая".
        /// </summary>
        /// <returns></returns>
        public abstract Task<IEnumerable> GetDynamicDataNewRequests();

        /// <summary>
        /// Метод получает список заявок со статусом "В работе".
        /// </summary>
        /// <returns></returns>
        public abstract Task<IEnumerable> GetDynamicDataWorkRequests();

        /// <summary>
        /// Метод получает список заявок со статусом "Требует подтверждения удаления".
        /// </summary>
        /// <returns></returns>
        public abstract Task<IEnumerable> GetDynamicDataAcceptDeleteRequests();

        /// <summary>
        /// Метод получает материалы требующие пополнения.
        /// </summary>
        /// <returns></returns>
        public abstract Task<IEnumerable> GetDynamicDataRefillMaterials();

        /// <summary>
        /// Метод получает материалы требующие сопоставления.
        /// </summary>
        /// <returns></returns>
        public abstract Task<IEnumerable> GetDynamicDataMappingMaterials();
    }
}

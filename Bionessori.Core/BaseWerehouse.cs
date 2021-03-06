﻿using Bionessori.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core {
    public abstract class BaseWerehouse {
        /// <summary>
        /// Метод получает список атериалов.
        /// </summary>
        /// <returns>Список с материалами.</returns>
        public abstract Task<IEnumerable> GetMaterials();

        /// <summary>
        /// Метод получает названия складов.
        /// </summary>
        /// <returns>Список названий складов.</returns>
        public abstract Task<IEnumerable> GetNameWerehouses();

        /// <summary>
        /// Метод выбирает названия групп.
        /// </summary>
        /// <returns>Список названий групп.</returns>
        public abstract Task<IEnumerable> GetGroupsWerehouses();

        /// <summary>
        /// Метод получает единицы измерения.
        /// </summary>
        /// <returns>Список с единицами измерения.</returns>
        public abstract Task<IEnumerable> GetMeasuresWerehouses();

        /// <summary>
        /// Метод получает материалы без дубликатов.
        /// </summary>
        /// <returns>Список названий материалов без дубликатов.</returns>
        public abstract Task<IEnumerable> GetDistinctMaterials();

        /// <summary>
        /// Метод выбирает материалы определенной группы.
        /// </summary>
        /// <returns>Список материалов определенной группы.</returns>
        public abstract Task<IEnumerable> GetMaterialsGroup(string group);

        /// <summary>
        /// Метод получает кол-во заявок со статусом "Новая".
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        public abstract Task<int> GetCountNewRequests();

        /// <summary>
        /// Метод получает кол-во заявок со статусом "В работе".
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        public abstract Task<int> GetCountRequestInWork();

        /// <summary>
        /// Метод получает кол-во материалов со статусом "Требует пополнения.".
        /// </summary>
        /// <returns>Кол-во материалов.</returns>
        public abstract Task<int> GetCountRefillMaterials();

        /// <summary>
        /// Метод получает кол-во материалов со статусом "Требует сопоставления.".
        /// </summary>
        /// <returns>Кол-во материалов.</returns>
        public abstract Task<int> GetCountMappingMaterials();

        /// <summary>
        /// Метод получает кол-во заявок со статусом "Ожидает подтверждения удаления".
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        public abstract Task<int> GetCountAcceptDeleteRequests();

        /// <summary>
        /// Метод создает новую номенклатуру.
        /// </summary>
        /// <param name="werehouse"></param>
        /// <returns></returns>
        public abstract Task CreateNomenclature(Werehouse werehouse);
    }
}
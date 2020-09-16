using Bionessori.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core {
    /// <summary>
    /// Базовый абстрактный класс отдела закупок.
    /// </summary>
    public abstract class BasePurchases {
        /// <summary>
        /// Метод получает список заявок.
        /// </summary>
        /// <returns></returns>
        public abstract Task<IEnumerable> GetRequests();

        /// <summary>
        /// Метод формирует новое коммерческое предложение поставщику.
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        public abstract Task FormOfferNoTemplate(object offer);

        /// <summary>
        /// Метод загружает данные заявки.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public abstract Task<IEnumerable<Request>> GetDataRequest(int number);
    }
}

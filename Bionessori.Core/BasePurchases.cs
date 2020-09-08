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
    }
}

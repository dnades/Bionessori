using Bionessori.Models;
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
    }
}

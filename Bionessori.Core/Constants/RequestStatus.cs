using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Core.Constants {
    /// <summary>
    /// Класс описывает набор констант статусов заявок.
    /// </summary>
    public class RequestStatus {
        public const string REQ_STATUS_NEW = "Новая";

        public const string REQ_STATUS_IN_WORK = "В работе";

        public const string REQ_STATUS_NEED_REFILL = "Требует пополнения";

        public const string REQ_STATUS_NEED_MAPPING = "Требует сопоставления";

        public const string REQ_STATUS_NEED_ACCEPT_DELETE = "Требует подтверждения удаления";

        public const string REQ_STATUS_ACCEPT = "Подтверждена";
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Класс описывает оповещения.
    /// </summary>
    public class Notification {
        public int Id { get; set; }

        public string Message { get; set; } // Сообщение оповещения.

        public string Category { get; set; }    // Категория оповещения.

        public string Module { get; set; }  // Название модуля.
    }
}

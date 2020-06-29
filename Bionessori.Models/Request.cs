using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает заявку.
    /// </summary>
    public class Request {
        public int Id { get; set; }

        public string Number { get; set; }  // Номер заявки.

        public int Count { get; set; }

        public string Measure { get; set; } // Ед.Изм.

        public string Status { get; set; }  // Статус заявки.

        public string Material { get; set; }    // Название материала.
    }
}

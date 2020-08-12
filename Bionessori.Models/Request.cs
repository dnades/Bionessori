using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает заявку.
    /// </summary>
    public class Request {
        public int Id { get; set; }

        public int Number { get; set; }  // Номер заявки.

        public int Count { get; set; }  // Кол-во материала.

        public string Measure { get; set; } // Ед.Изм.

        public string Status { get; set; }  // Статус заявки.

        public IEnumerable<string> Material { get; set; }    // Список материалов.

        public string MaterialGroup { get; set; }   // Группа, к которой относится материал.
    }
}

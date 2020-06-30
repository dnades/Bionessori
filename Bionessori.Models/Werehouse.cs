using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает склад.
    /// </summary>
    public class Werehouse {
        public int Id { get; set; }

        public string Material { get; set; }    // Материал склада.

        public string MaterialGroup { get; set; }

        public string Measure { get; set; } // Ед.Изм.

        public int Count { get; set; }

        public Guid Code { get; set; }    // Код материала.

        public string WerehouseNumber { get; set; }    // Номер склада.
    }
}

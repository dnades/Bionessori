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

        public int Reserve { get; set; }    // В резерве.

        public string VendorCode { get; set; }  // Артикул материала.

        public string WerehouseNumber { get; set; }    // Номер склада.

        public double Price { get; set; }  // Цена.

        public double TotalSum { get; set; }   // Сумма.

        public string ProviderName { get; set; } // Наименование поставщика.

        public double Percentage { get; set; }  // Процент скидки.

        public double VAT { get; set; } // НДС.
    }
}
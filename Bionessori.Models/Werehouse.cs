using Bionessori.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает склад.
    /// </summary>
    public class Werehouse {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("material")]
        public string Material { get; set; }    // Материал склада.

        [Column("material_group")]
        public string MaterialGroup { get; set; }

        [Column("measure")]
        public string Measure { get; set; } // Ед.Изм.

        [Column("count")]
        public int Count { get; set; }

        [Column("reserve")]
        public int Reserve { get; set; }    // В резерве.

        [Column("vendor_code")]
        public int VendorCode { get; set; }  // Артикул материала.

        [Column("werehouse_number")]
        public string WerehouseNumber { get; set; }    // Номер склада.

        [Column("price")]
        public decimal Price { get; set; }  // Цена.

        [Column("total_sum")]
        public decimal TotalSum { get; set; }   // Сумма.

        [NotMapped, Column("provider_name")]
        public string ProviderName { get; set; } // Наименование поставщика.

        [Column("percentage")]
        public decimal Percentage { get; set; }  // Процент скидки.

        [Column("vat")]
        public decimal VAT { get; set; } // НДС.

        public List<MultepleContextTable> MultepleContextTables { get; set; }

        public Werehouse() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
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

        [Column("code")]
        public string Code { get; set; }    // Код материала.

        [Required(ErrorMessage = "Не заполнен материал"), Column("material")]
        public string Material { get; set; }    // Материал склада.

        [Required(ErrorMessage = "Не заполнена группа"), Column("material_group")]
        public string MaterialGroup { get; set; }   // Группа в которую входит материал.

        [Column("measure")]
        public string Measure { get; set; } // Ед.Изм.

        [Column("count")]
        public int Count { get; set; }  // Кол-во материала.

        [Column("weight")]
        public string Weight { get; set; }   // Вес материала.

        [Column("weight_measurement")]
        public string WeightMeasurement { get; set; }   // Ед.изм. веса.

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

        [NotMapped, Column("provider_id")]
        public string ProviderId { get; set; } // Наименование поставщика.

        [Column("percentage")]
        public string Percentage { get; set; }  // Процент скидки.

        [Column("vat")]
        public string VAT { get; set; } // НДС.

        public List<MultepleContextTable> MultepleContextTables { get; set; }

        public Werehouse() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
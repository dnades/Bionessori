using Bionessori.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает таблицу коммерческих предложений поставщикам.
    /// </summary>
    public class CommerceOffer {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("material")]
        public string Material { get; set; }

        [Column("group")]
        public string Group { get; set; }

        [Column("measure")]
        public string Measure { get; set; }

        [Column("count")]
        public int Count { get; set; }

        [Column("date")]
        public string Date { get; set; }
        
        [Column("max_sum")]
        public string MaxSum { get; set; } // Максимальная сумма поставщику за материал.

        [Column("status")]
        public string Status { get; set; }  // Статус предложения.

        [Column("offer_code")]
        public int OfferCode { get; set; }  // Код предложения.

        [Column("best_provider_id")]
        public int BestProviderId { get; set; } // Id выбранного поставщика.

        public List<MultepleContextTable> MultepleContextTables { get; set; }

        public CommerceOffer() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}

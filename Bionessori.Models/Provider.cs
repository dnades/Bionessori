using Bionessori.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bionessori.Models {
    public class Provider {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("provider_name")]
        public string ProviderName { get; set; }

        public List<MultepleContextTable> MultepleContextTables { get; set; }

        public Provider() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}

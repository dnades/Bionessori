using Bionessori.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает заявку.
    /// </summary>
    public class Request {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("number")]
        public int Number { get; set; }  // Номер заявки.

        [Column("count")]
        public int Count { get; set; }  // Кол-во материала.

        [Column("measure")]
        public string Measure { get; set; } // Ед.Изм.

        [Column("status")]
        public string Status { get; set; }  // Статус заявки.

        [Column("material")]
        public string Material { get; set; }    // Список материалов. 

        [Column("material_group")]
        public string MaterialGroup { get; set; }   // Группа, к которой относится материал.

        public List<MultepleContextTable> MultepleContextTables { get; set; }

        public Request() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
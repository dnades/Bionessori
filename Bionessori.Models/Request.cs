using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает заявку.
    /// </summary>
    public class Request {
        public int Id { get; set; }

        public string Number { get; set; }

        public int Count { get; set; }

        public string Measure { get; set; }

        public string Status { get; set; }

        public int MaterialId { get; set; }

        public string Name { get; set; } 
    }
}

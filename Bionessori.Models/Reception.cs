using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает запись на прием.
    /// </summary>
    public class Reception {
        public int Id { get; set; }

        public string Date { get; set; }    // Дата приема.

        public int NumberReception { get; set; }    // Номер записи.

        public int EmployeeId { get; set; } // Id сотрудника, который ведет прием.

        public string CardNumber { get; set; }  // Номер карты пациента.

        public string FullName { get; set; }    // ФИО врача.
    }
}

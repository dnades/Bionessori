using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает направление пациента.
    /// </summary>
    public class Direction {
        public int Id { get; set; }

        public string PatientName { get; set; }  // Пациент.

        public string NumberDirection { get; set; } // Номер направления.

        public string SeatDirection { get; set; }    // Id названия направления.
    }
}
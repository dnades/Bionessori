using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает расписание.
    /// </summary>
    public class Schedule {
        public int Id { get; set; }

        public string DateSchedule { get; set; }    // Дата расписания.

        public string EmployeeName { get; set; }   // Логин пользователя.

        public string Status { get; set; }  // Статус работает, отменен, в отпуске.
    }
}

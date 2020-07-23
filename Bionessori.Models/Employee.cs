using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает сотрудника.
    /// </summary>
    public class Employee {
        public int Id { get; set; }

        public string FullName { get; set; }    // ФИО сотрудника.

        public string Login { get; set; }   // Логин сотрудника в системе.

        public string Position { get; set; }    // Специализация или должноть сотрудника.

        public string Address { get; set; } // Адрес сотрудника.

        public string PostCode { get; set; }    // Почтовый индекс сотрудника.

        public string Number { get; set; }  // Конт.номер сотрудника.

        public string TabNumber { get; set; }   // Табельный номер сотрудника (если есть).

        public DateTime DateBirth { get; set; } // Дата рождения сотрудника.

        public DateTime StartDateWork { get; set; } // Дата найма сотрудника.

        public string PasportNumber { get; set; }   // Серия и номер паспрта сотрудника.

        public int Age { get; set; }

        public int NumberSeatWork { get; set; } // Номер стационара или поликлиники, в котором работает сотрудник.

        public int DepartmentId { get; set; }   // Уникальный номер отделения, в котором работает сотрудник.

        public int UserId { get; set; } // Id пользователя системы, под которым сидит сотрудник.
    }
}

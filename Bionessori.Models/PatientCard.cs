using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает карту пациента.
    /// </summary>
    public class PatientCard {
        public int Id { get; set; }

        public string CardNumber { get; set; } // Номер карты пациента.

        public string FullName { get; set; }    // ФИО пациента.

        public DateTime DateOfBirth { get; set; }   // Дата рождения пациента.

        public string Address { get; set; } // Адрес пациента.

        public string Number { get; set; }  // Номер телефона пациента.

        public string Email { get; set; }

        public string Plan { get; set; }

        public string BloodGroup { get; set; } // Группа крови.

        public string Policy { get; set; }  // Полис пациента. Должен быть в виде 111111 1111111111

        public string Snails { get; set; }  // СНИЛС пациента. Должен быть в виде 111-111-111 11

        public DateTime TimeProcRecommend { get; set; } // Время записи на процедуры.

        public string PrescriptionDrugs { get; set; }   // Прописанные лекарства.

        public string Diagnosis { get; set; }   // Диагноз.

        public string RecipesRecommend { get; set; }    // Рекомендации по лечению.

        public string MedicalHistory { get; set; }  // История болезни.

        public string Doctor { get; set; }  // Лечащий доктор.

        public string Category { get; set; }    // Категория пациента.

        public string SeatWord { get; set; }    // Место работы пациента.

        public string Position { get; set; }    // Должность пациента.

        public string TabNum { get; set; }  // Табельный номер пациента (если есть).

        public string InsuranceCompany { get; set; }    // Страховая компания.

        public DateTime DateTo { get; set; }    // Обслуж.до

        public string Comment { get; set; } // Комментарии.

        public string Indicator { get; set; }   // Сигнальная информация.

        public string isVich { get; set; }  // ВИЧ.

        public string isHb { get; set; }    // Hb.

        public string isRw { get; set; }    // Rw.

        public string City { get; set; }    // Город.

        public string DopAddress { get; set; }  // Дополнительный адрес.

        public string Dop { get; set; } // Дополнительное описание.

        public string District { get; set; }    // Район.

        public string Region { get; set; }  // Регион.

        public string FormPay { get; set; } // Форма оплаты.

        public string Registry { get; set; }    // Зарегистрирован.

        public string WhoChange { get; set; }   // Изменен.

        public string Operator { get; set; }    // Оператор.
    }
}

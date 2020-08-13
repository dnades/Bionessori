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

        public int CardNumber { get; set; } // Номер карты пациента.

        public string FullName { get; set; }    // ФИО пациента.

        public string DateOfBirth { get; set; }   // Дата рождения пациента.

        public string Address { get; set; } // Адрес пациента.

        public string Number { get; set; }  // Номер телефона пациента.

        public string Email { get; set; }

        public string PlanPay { get; set; }

        public string BloodGroup { get; set; } // Группа крови.

        public string Policy { get; set; }  // Полис пациента. Должен быть в виде 1111 0000 0000 0000

        public string Snails { get; set; }  // СНИЛС пациента. Должен быть в виде 111-111-111 11

        public string TimeProcRecommend { get; set; } // Время записи на процедуры.   2019-01-06T17:16:40

        public string PrescriptionDrugs { get; set; }   // Прописанные лекарства.

        public string Diagnosis { get; set; }   // Диагноз.

        public string RecipesRecommend { get; set; }    // Рекомендации по лечению.

        public string MedicalHistory { get; set; }  // История болезни.

        public string Doctor { get; set; }  // Лечащий доктор.

        public string Category { get; set; }    // Категория пациента.

        public string SeatWork { get; set; }    // Место работы пациента.

        public string Position { get; set; }    // Должность пациента.

        public string TabNum { get; set; }  // Табельный номер пациента (если есть).

        public string InsuranceCompany { get; set; }    // Страховая компания.

        public string DateTo { get; set; }    // Обслуж.до    2019-01-06T17:16:40

        public string Comment { get; set; } // Комментарии.

        public string Indicator { get; set; }   // Сигнальная информация.

        public string isVich { get; set; }  // ВИЧ.

        public string isHb { get; set; }    // Hb.

        public string isRw { get; set; }    // Rw.

        public string City { get; set; }    // Город.

        public string District { get; set; }    // Район.

        public string Region { get; set; }  // Регион.

        public string FormPay { get; set; } // Форма оплаты.

        public string Registry { get; set; }    // Зарегистрирован.   2019-01-06T17:16:40

        public string WhoChange { get; set; }   // Изменен.

        public string Operator { get; set; }    // Оператор.

        public string IndexNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает карту пациента.
    /// </summary>
    public class PatientCard {
        public int Id { get; set; }

        public string FullName { get; set; }    // ФИО пациента.

        public DateTime DateOfBirth { get; set; }   // Дата рождения пациента.

        public string Address { get; set; } // Адрес пациента.

        public string Number { get; set; }  // Номер телефона пациента.

        public string Policy { get; set; }  // Полис пациента. Должен быть в виде 111111 1111111111

        public string Snails { get; set; }  // СНИЛС пациента. Должен быть в виде 111-111-111 11

        public DateTime TimeProcRecommend { get; set; } // Время записи на процедуры.

        public string PrescriptionDrugs { get; set; }   // Прописанные лекарства.

        public string Diagnosis { get; set; }   // Диагноз.

        public string RecipesRecommend { get; set; }    // Рекомендации по лечению.

        public string MedicalHistory { get; set; }  // История болезни.
    }
}

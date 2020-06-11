using Bionessori.Core.Interfaces;
using Bionessori.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис реализует методы работы регистратуры.
    /// </summary>
    public class RegistryService : IRegistry {
        /// <summary>
        /// Метод вызывает скорую помощь.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public Task<PatientCard> Call(PatientCard patient) {
            throw new NotImplementedException();
        }

        // TODO: пока не трогать, так как пока не придумал, куда регистратура может отправлять карту.
        public Task<PatientCard> Send(PatientCard patient) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод записывает на прием.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public Task<PatientCard> Write(PatientCard patient) {
            throw new NotImplementedException();
        }
    }
}

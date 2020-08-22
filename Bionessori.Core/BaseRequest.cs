using Bionessori.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core {
    public abstract class BaseRequest {
        /// <summary>
        /// Метод создает новую заявку.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract Task CreateRequest(object request);

        /// <summary>
        /// Метод получает список всех заявок.
        /// </summary>
        /// <returns>Список с заявками.</returns>
        public abstract Task<IEnumerable> GetRequests();
    }
}

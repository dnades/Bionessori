using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис реализует методы складов.
    /// </summary>
    public class WerehouseService : IWerehouse {
        string _connectionString = null;

        public WerehouseService(string conStr) {
            _connectionString = conStr;
        }

        /// <summary>
        /// Метод получает список продуктов.
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetProducts() {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage httpResponse = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://online.moysklad.ru/api/remap/1.1/entity/product"),
                Headers = {
                { HttpRequestHeader.Authorization.ToString(), "Basic YWRtaW5Ac2llcnJhXzkzMToJN2MzZTQ5YTU0OQ==" },
                { HttpRequestHeader.Accept.ToString(), "application/json" },
                }
            };

            // Отправляет запрос.
            var response = httpClient.SendAsync(httpResponse).Result;

            // Получает результат запроса со списком.
            var httpResponseBody = await response.Content.ReadAsStringAsync();

            return httpResponseBody;
        }
    }
}

using Bionessori.Core;
using Bionessori.Core.Constants;
using Bionessori.Core.Data;
using Bionessori.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис реализует методы работы отдела закупок.
    /// </summary>
    public class PurchasesService : BasePurchases {
        ApplicationDbContext _db;
        public PurchasesService(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Метод получает список заявок.
        /// </summary>
        /// <returns>Список заявок.</returns>
        public async override Task<IEnumerable> GetRequests() {
            try {
                var oRequests = await _db.Requests
                    .Where(r => r.Status.Equals(RequestStatus.REQ_STATUS_IN_WORK) ||
                    r.Status.Equals(RequestStatus.REQ_STATUS_ACCEPT) ||
                    r.Status.Equals(RequestStatus.REQ_STATUS_COMPLETE)).ToListAsync();

                return oRequests;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод формирует новое коммерческое предложение поставщику.
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        public async override Task FormOfferNoTemplate(object offer) {
            string typeParam = "request";
            int generateNumber = 0;
            try {
                var objParse = JsonSerializer.Serialize(offer);
                JObject jParse = JObject.Parse(objParse);

                var aMaterials = (JArray)jParse["aMaterials"];
                var aMaterialValues = aMaterials.Values().ToList();

                var aGroups = (JArray)jParse["aGroups"];
                var aGroupValues = aGroups.Values().ToList();

                var aMeasures = (JArray)jParse["aAddMeasures"];
                var aMeasureValues = aMeasures.Values().ToList();

                var aCount = (JArray)jParse["aCountMaterials"];
                var aCountValues = aCount.Values().ToList();

                var aDates = (JArray)jParse["aDates"];
                var aDateValues = aDates.Values().ToList();

                var aSum = (JArray)jParse["aSums"];
                var aSumValues = aSum.Values().ToList();

                // Генерит рандомный номер предложения.
                int RandomGenerate() {
                    return RandomDataService.GenerateRandomNumber();
                }

                generateNumber = RandomGenerate();

                int i = 0;
                foreach (var material in aMaterialValues) {
                    CommerceOffer offerObject = new CommerceOffer() {
                        OfferCode = generateNumber,
                        Status = CommerceOfferStatus.OFFER_STATUS_PREPARATION
                    };

                    // Итеративно создает объекты предложений.
                    offerObject.Material = material.ToString();
                    offerObject.Group = aGroupValues[i].ToString();
                    offerObject.Count = Convert.ToInt32(aCount[i]);
                    offerObject.Measure = aMeasures[i].ToString();
                    offerObject.Date = aDateValues[i].ToString();
                    
                    // Форматирует цену.
                    long result = (long)aSumValues[i];
                    offerObject.MaxSum = result.ToString("N");

                    await _db.CommerceOffers.AddRangeAsync(offerObject);
                    i++;
                }
                await _db.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}

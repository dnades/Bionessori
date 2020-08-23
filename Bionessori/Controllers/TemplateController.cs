using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core;
using Bionessori.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionessori.Controllers {
    /// <summary>
    /// Контроллер описывает методы работы с различными шаблонами.
    /// </summary>
    [ApiController, Route("api/template")]
    public class TemplateController : ControllerBase {
        ApplicationDbContext _db;        
        public TemplateController(ApplicationDbContext db) {
            _db = db;
        }

        
    }
}

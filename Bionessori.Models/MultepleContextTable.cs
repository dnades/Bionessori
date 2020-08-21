using Bionessori.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bionessori.Core.Data {
    public class MultepleContextTable {
        public int RequestId { get; set; }
        public Request Request { get; set; }

        public int WerehouseId { get; set; }
        public Werehouse Werehouse { get; set; }
    }
}
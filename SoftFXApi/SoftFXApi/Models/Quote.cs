using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftFXApi.Models
{
    public class Quote
    {
        public int Id { get; set; }

        public int SymbolFk { get; set; }
        [ForeignKey("SymbolFk")]
        public Symbol Symbol { get; set; }
        public DateTime DateTime { get; set; }
        public int Volume { get; set; }

    }
}
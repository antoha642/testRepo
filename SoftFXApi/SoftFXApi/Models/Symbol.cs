using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SoftFXApi.Models
{
    public class Symbol
    {
        public int SymbolId { get; set; }
        [JsonProperty("Symbol")]
        public string Name { get; set; }
        //public ICollection<Quote> Quotes { get; set; }
    }
}
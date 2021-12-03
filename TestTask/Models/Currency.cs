using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class Currency
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string EngName { get; set; }
        public int Nominal { get; set; }
        public string ParentCode { get; set; }
        public int ISO_Num_Code { get; set; }
        public string ISO_Char_Code { get; set; }


        public List<CurrencyRate> currencyRates { get; set; } = new List<CurrencyRate>();

    }
}

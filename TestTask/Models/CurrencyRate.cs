using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class CurrencyRate
    {

        public int Id { get; set; }
        public DateTime date { get; set; }
        public decimal Value { get; set; }
        [Required]
        public string CurrencyId { get; set; }
        public Currency currency { get; set; }

    }
}

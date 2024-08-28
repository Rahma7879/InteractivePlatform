using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractivePlatform.DAL.Model
{
    public class FinanceRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RequestNumber { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }

        [Required]
        public int PaymentPeriod { get; set; } 

        [Required]
        public decimal TotalProfit { get; set; }

        [Required]
        public string Status { get; set; }
    }
}

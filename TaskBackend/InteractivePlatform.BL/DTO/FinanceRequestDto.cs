using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractivePlatform.BL.DTO
{
    public class FinanceRequestDto
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; }
        public decimal PaymentAmount { get; set; }
        public int PaymentPeriod { get; set; }
        public decimal TotalProfit { get; set; }
        public string RequestStatus { get; set; }
    }
}

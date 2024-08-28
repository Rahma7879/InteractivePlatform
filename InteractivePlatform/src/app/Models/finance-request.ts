export class FinanceRequest {
    constructor(
       public  id: number,
       public  requestNumber: string,
       public paymentAmount: number,
       public  paymentPeriod: number,
       public  totalProfit: number,
       public  status: string
      ){}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senit.Core.Enums
{
    public enum TransactionType
    {
        Payment,
        CashIn,
        CashOut,
        MerchantPayment,
        IncomingRemittance,
        OutgoingRemittance,
        AirtimeTopUp
    }
}

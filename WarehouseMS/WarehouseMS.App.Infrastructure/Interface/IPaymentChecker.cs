using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseMS.App.Infrastructure.Interface
{
    public interface IPaymentChecker
    {
        Task<bool> IsOrderPaidAsync(string orderId, string customerId, CancellationToken ct = default);
    }
}

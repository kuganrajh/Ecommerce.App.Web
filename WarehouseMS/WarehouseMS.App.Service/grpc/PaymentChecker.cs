using Grpc.Core;
using Shared.Grpc.Contract.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMS.App.Infrastructure.Interface;

namespace WarehouseMS.App.Service.grpc
{
    public class PaymentChecker : IPaymentChecker
    {
        private readonly PaymentService.PaymentServiceClient _client;

        public PaymentChecker(PaymentService.PaymentServiceClient client)
        {
            _client = client;
        }

        public async Task<bool> IsOrderPaidAsync(string orderId, string customerId, CancellationToken ct = default)
        {
            // deadline = hard timeout for gRPC call
            var deadline = DateTime.UtcNow.AddSeconds(3);

            try
            {
                var result  =  await _client.IsOrderPaidAsync(
                    new IsOrderPaidRequest
                    {
                        OrderId = orderId,
                        CustomerId = customerId
                    },
                    deadline: deadline,
                    cancellationToken: ct
                );
                return result.IsPaid;
            }
            catch (RpcException ex) when (ex.StatusCode is StatusCode.DeadlineExceeded)
            {
                // Payment service slow/unreachable
                return false;
            }
            catch (RpcException ex)
            {
                // gRPC-level failure (Unavailable, Internal, etc.)
                return false;
            }
        }
    }
}

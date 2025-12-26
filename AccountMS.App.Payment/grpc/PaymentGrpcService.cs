using Grpc.Core;
using Shared.Grpc.Contract.V1;


namespace AccountMS.App.Payment.grpc
{
    public class PaymentGrpcService : PaymentService.PaymentServiceBase
    {
        public override Task<IsOrderPaidResponse> IsOrderPaid(
            IsOrderPaidRequest request,
            ServerCallContext context)
        {
            // TODO: replace with real DB / repository check
            // Example logic:
            var isPaid = request.OrderId == "1" && request.CustomerId == "1";

            var response = new IsOrderPaidResponse
            {
                IsPaid = isPaid,
                PaymentId = isPaid ? "PAY-12345" : string.Empty,
                Status = isPaid ? "PAID" : "NOT_PAID"
            };

            return Task.FromResult(response);
        }
    }
}

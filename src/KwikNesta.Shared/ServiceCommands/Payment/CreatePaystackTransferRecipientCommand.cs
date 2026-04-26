using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceCommands.Payment
{
    public class CreatePaystackTransferRecipientCommand : IKNRequest<Response<CreatePaystackTransferResponse>>
    {
        public string AccountType { get; set; } = default!;
        public string BankName { get; set; } = default!;
        public string AccountNumber { get; set; } = default!;
        public string BankCode { get; set; } = default!;
        public string Currency { get; set; } = default!;
    }
}
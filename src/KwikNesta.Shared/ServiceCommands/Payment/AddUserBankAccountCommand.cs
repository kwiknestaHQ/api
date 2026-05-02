using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Payment
{
    public class AddUserBankAccountCommand : AddUserBankAccountRequest, IKNRequest<Response<string>>
    {
        public UserContext Context { get; set; } = default!;
    }

    public class AddUserBankAccountRequest
    {
        public string BankName { get; set; } = default!;
        public string AccountNumber { get; set; } = default!;
        public string BankCode { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Currency { get; set; } = default!;
    }
}
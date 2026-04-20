using KwikNesta.Mediator.Cores.Abstractions;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class FinalizeViewRequestNotification : IKNNotification
    {
        public string Reference { get; set; } = default!;
    }
}
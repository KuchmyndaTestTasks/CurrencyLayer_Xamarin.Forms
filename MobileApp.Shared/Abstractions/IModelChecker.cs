using MobileApp.Shared.Infrastructure.MainOperations;

namespace MobileApp.Shared.Abstractions
{
    interface IModelChecker
    {
        Logger.MessageLog Message { get; }
        bool IsValid();
        void ResetState();
    }
}

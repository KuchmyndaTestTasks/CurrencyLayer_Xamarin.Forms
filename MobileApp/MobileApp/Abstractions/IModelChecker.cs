using MobileApp.Infrastructure.MainOperations;

namespace MobileApp.Abstractions
{
    interface IModelChecker
    {
        Logger.MessageLog Message { get; }
        bool IsValid();
        void ResetState();
    }
}

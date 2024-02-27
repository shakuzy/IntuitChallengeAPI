using System.Runtime.CompilerServices;

namespace IntuitChallengeAPI.Clases.Injections
{
    public interface ILogger
    {
        Task GuardarLog(string message, string callerName, bool DesdeOtroLog = false);
        Task GuardarError(string message, [CallerMemberName] string? callerName = "");
        Task GuardarImportante(string message, [CallerMemberName] string? callerName = "");
    }
}

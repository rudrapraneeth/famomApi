using HomeMade.Core.ViewModels;
using System.Threading.Tasks;

namespace HomeMade.Core.Interfaces
{
    public interface ITwilioService
    {
        Task<TwilioResult> StartVerificationAsync(string phoneNumber, string channel);

        Task<TwilioResult> CheckVerificationAsync(string phoneNumber, string code);
    }
}

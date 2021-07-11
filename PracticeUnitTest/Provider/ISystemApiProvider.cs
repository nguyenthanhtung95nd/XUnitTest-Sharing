using PracticeUnitTest.Provider.Entity;
using System.Threading.Tasks;

namespace PracticeUnitTest.Provider
{
    public interface ISystemApiProvider
    {
        Task<SystemApiResponse> GetSysInfoAsync(SystemApiRequest request);
    }
}
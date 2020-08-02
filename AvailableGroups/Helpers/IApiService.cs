using System.Threading.Tasks;

namespace AvailableGroups.Helpers
{
    public interface IApiService
    {
        Task<string> GetApiDataAsync(string protectedApiUrl, string access_Token);
    }
}
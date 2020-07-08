using System.Threading.Tasks;
using Refit;

namespace CityManager.Helper
{
    [Headers("ContentType: application/json")]
    public interface IRestApiClient<T, T1, in TKey> where T : class
    {
        [Get("/{**key}")]
        Task<T> Get(TKey key, T1 queryParam);
    }
}
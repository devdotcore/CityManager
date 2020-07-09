using System.Threading.Tasks;
using Refit;

namespace CityManager.Helper
{
    /// <summary>
    /// Refit helper method for calling http endpoint
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    [Headers("ContentType: application/json")]
    public interface IRestApiClient<T, T1, in TKey> where T : class
    {
        [Get("/{**key}")]
        Task<T> Get(TKey key, T1 queryParam);
    }
}
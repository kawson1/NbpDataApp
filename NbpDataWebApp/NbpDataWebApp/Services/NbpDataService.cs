using NbpDataWebApp.Models;

namespace NbpDataWebApp.Services;

public interface INbpDataService
{
    public Task<ExchangeData> GetMajorBuySellDifference(string currencyCode, int count);

    public Task<ExchangeData> GetSingleExchange(string currencyCode, string exchangeDate);

    public Task<(ExchangeData e1, ExchangeData e2)> GetMinMaxExchanges(string currencyCode, int count);

}

public class NbpDataService : INbpDataService
{
    private HttpClient _httpClient;

    public NbpDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ExchangeData> GetMajorBuySellDifference(string currencyCode, int count)
    {
        var response = await _httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/c/{currencyCode}/last/{count}/");
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return ExchangeDataHelper.GetBuySellDiffFromJson(responseString);
        }
        else
            return null;
    }

    public async Task<ExchangeData> GetSingleExchange(string currencyCode, string exchangeDate)
    {
        var response = await _httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/a/{currencyCode}/{exchangeDate}/");

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var ex = ExchangeDataHelper.GetDataFromJson(responseString);
            return ex;
        }
        else
            return null;
    }

    public async Task<(ExchangeData e1, ExchangeData e2)> GetMinMaxExchanges(string currencyCode, int count)
    {
        var response = await _httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/a/{currencyCode}/last/{count}/");
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var (min, max) = ExchangeDataHelper.GetMinMaxFromJson(responseString);
            return (min, max);
        }
        else
            return (null, null);
    }
}
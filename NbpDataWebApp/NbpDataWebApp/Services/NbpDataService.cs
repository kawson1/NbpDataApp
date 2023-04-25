using NbpDataWebApp.Models;

namespace NbpDataWebApp.Services;

public interface INbpDataService
{
    public Task<string> GetMajorBuySellDifference(string currencyCode, int count);

    public Task<string> GetSingleExchange(string currencyCode, string exchangeDate);

    public Task<string> GetMinMaxExchanges(string currencyCode, int count);

}

public class NbpDataService : INbpDataService
{
    private HttpClient _httpClient;

    public NbpDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> GetMajorBuySellDifference(string currencyCode, int count)
    {
        var response = await _httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/c/{currencyCode}/last/{count}/");
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            ExchangeData exchange = ExchangeDataHelper.GetBuySellDiffFromJson(responseString);
            return $"Major difference between buy & sell: {exchange.ask - exchange.bid}:\n\t" +
                   $"Effective data: {exchange.effectiveDate.ToShortDateString()}\n\t" +
                   $"Exchange bid: {exchange.bid}\n\t" +
                   $"Exchange ask: {exchange.ask}";
        }
        else
            return $"Couldn't get data";
    }

    public async Task<string> GetSingleExchange(string currencyCode, string exchangeDate)
    {
        var response = await _httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/a/{currencyCode}/{exchangeDate}/");
            
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                ExchangeData ex = ExchangeDataHelper.GetDataFromJson(responseString);
                return $"Exchange code: {ex.currencyCode}\nAverage exchange rate: {ex.exchangeRate}";
            }
            else
                return $"Couldn't get data";
    }

    public async Task<string> GetMinMaxExchanges(string currencyCode, int count)
    {
        var response = await _httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/a/{currencyCode}/last/{count}/");
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var (min, max) = ExchangeDataHelper.GetMinMaxFromJson(responseString);
            return $"Minimal exchange data for {currencyCode}:\n\t" +
                   $"Effective data: {min.effectiveDate.ToShortDateString()}\n\t" +
                   $"Exchange rate: {min.exchangeRate}\n" +
                   
                   $"Maximal exchange data for {currencyCode}:\n\t" +
                   $"Effective data: {max.effectiveDate.ToShortDateString()}\n\t" +
                   $"Exchange rate: {max.exchangeRate}";
        }
        else
            return $"Couldn't get data";
    }
}
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NbpDataWebApp.Models;

namespace NbpDataWebApp.Controllers;

[ApiController]
[Route("NbpData")]
public class NbpDataController : Controller
{
    // TODO: handle if there is no data for specific day
    // TODO: handle if exchangeDate is correct format (yyyy-mm-dd)
    [HttpGet]
    [Route("exchanges/{currencyCode}/{exchangeDate:datetime}")]
    public async Task<string> Exchange(string currencyCode, string exchangeDate)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/a/{currencyCode}/{exchangeDate}/");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                ExchangeData ex = ExchangeDataModel.GetDataFromJson(responseString);
                return $"Exchange code: {ex.currencyCode}\nAverage exchange rate: {ex.exchangeRate}";
            }
            else
                return $"Couldn't get data";
        }
    }
    
    [HttpGet]
    [Route("exchanges/{currencyCode}/{count:int}")]
    public async Task<string> MinMaxExchange(string currencyCode, int count)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/a/{currencyCode}/last/{count}/");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var (min, max) = ExchangeDataModel.GetMinMaxFromJson(responseString);
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
    
    [HttpGet]
    [Route("buyselldiff/{currencyCode}/{count:int}")]
    public async Task<string> BiggestBuySellDifference(string currencyCode, int count)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/c/{currencyCode}/last/{count}/");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                ExchangeData exchange = ExchangeDataModel.GetBuySellDiffFromJson(responseString);
                return $"Major difference between buy & sell: {exchange.ask - exchange.bid}:\n\t" +
                       $"Effective data: {exchange.effectiveDate.ToShortDateString()}\n\t" +
                       $"Exchange bid: {exchange.bid}\n\t" +
                       $"Exchange ask: {exchange.ask}";
            }
            else
                return $"Couldn't get data";
        }
    }
}
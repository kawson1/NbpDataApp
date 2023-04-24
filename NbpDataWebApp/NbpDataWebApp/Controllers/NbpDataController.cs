using System.Net;
using Microsoft.AspNetCore.Mvc;
using NbpDataWebApp.Models;

namespace NbpDataWebApp.Controllers;

[ApiController]
[Route("NbpData")]
public class NbpDataController : Controller
{
    [HttpGet]
    [Route("exchanges/{exchangeDate}")]
    public async Task<string> Exchange(string exchangeDate)
    {
        Console.WriteLine(exchangeDate);
        using (HttpClient httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync("http://api.nbp.pl/api/exchangerates/rates/a/AUD/today/");
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
}
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NbpDataWebApp.Models;
using NbpDataWebApp.Services;

namespace NbpDataWebApp.Controllers;

[ApiController]
[Route("NbpData")]
public class NbpDataController : Controller
{
    private INbpDataService _nbpDataService;

    public NbpDataController(INbpDataService nbpDataService)
    {
        _nbpDataService = nbpDataService;
    }
    
    [HttpGet]
    [Route("exchanges/{currencyCode}/{exchangeDate:datetime}")]
    public async Task<string> Exchange(string currencyCode, string exchangeDate)
    {
        return await _nbpDataService.GetSingleExchange(currencyCode, exchangeDate);
    }
    
    [HttpGet]
    [Route("exchanges/{currencyCode}/{count:int}")]
    public async Task<string> MinMaxExchange(string currencyCode, int count)
    {
        return await _nbpDataService.GetMinMaxExchanges(currencyCode, count);
    }
    
    [HttpGet]
    [Route("buyselldiff/{currencyCode}/{count:int}")]
    public async Task<string> BiggestBuySellDifference(string currencyCode, int count)
    {
        return await _nbpDataService.GetMajorBuySellDifference(currencyCode, count);
    }
}
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
    public async Task<IActionResult> Exchange(string currencyCode, string exchangeDate)
    {
        var exchange = await _nbpDataService.GetSingleExchange(currencyCode, exchangeDate);
        return View(exchange);
    }
    
    [HttpGet]
    [Route("exchanges/{currencyCode}/{count:int}")]
    public async Task<IActionResult> MinMaxExchange(string currencyCode, int count)
    {
        var (e1, e2) = await _nbpDataService.GetMinMaxExchanges(currencyCode, count);
        var tuple = Tuple.Create(e1, e2);
        return View(tuple);
    }
    
    [HttpGet]
    [Route("buyselldiff/{currencyCode}/{count:int}")]
    public async Task<IActionResult> BiggestBuySellDifference(string currencyCode, int count)
    {
        var exchange = await _nbpDataService.GetMajorBuySellDifference(currencyCode, count);
        return View(exchange);
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NbpDataWebApp.Models;

public class ExchangeData
{ 
    public string currencyCode;
    public float exchangeRate;
    public float bid;
    public float ask;
    public DateTime effectiveDate;
}
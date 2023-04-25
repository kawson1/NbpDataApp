using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NbpDataWebApp.Models;

public class ExchangeDataHelper
{
        public static ExchangeData GetDataFromJson(string jsonString)
    {
        try
        {
            var json = JObject.Parse(jsonString);
            float exRate = float.Parse(JObject.Parse(jsonString).SelectToken("rates[0].mid").ToString());
            string currCode = json.SelectToken("code").ToString();

            var ex = new ExchangeData
            {
                currencyCode = currCode,
                exchangeRate = exRate,
                effectiveDate = DateTime.Parse(json.SelectToken("rates[0].effectiveDate").ToString())
            };

            return ex;
        }
        // Catch JSON parse error
        catch (JsonReaderException ex)
        {
            Console.WriteLine($"JSON parse error: {ex.Message}");
        }
        
        // Catch float parse error
        catch (FormatException ex)
        {
            Console.WriteLine($"Float parse error: {ex.Message}");
        }
        
        // Other exception
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        return null;
    }
    
    /// <summary>
    /// Returns ExchangeData tuple - minimal exchange value object and maximum exchange value object
    /// </summary>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static (ExchangeData Min, ExchangeData Max) GetMinMaxFromJson(string jsonString)
    {
        try
        {
            var jsonObject = JObject.Parse(jsonString);
            var rates = jsonObject.SelectToken("rates");
            
            Console.WriteLine(rates);
            List<ExchangeData> exchanges = 
                rates.AsEnumerable()
                    .Select(rate =>
            {
                return new ExchangeData
                {
                    currencyCode = "AUD",
                    exchangeRate = float.Parse(rate.SelectToken("mid").ToString()),
                    effectiveDate = DateTime.Parse(rate.SelectToken("effectiveDate").ToString())
                };
            })
                    .ToList();
            ExchangeData min = exchanges.MinBy(exchange => exchange.exchangeRate);
            ExchangeData max = exchanges.MaxBy(exchange => exchange.exchangeRate);
            return (min, max);
        }
        // Catch JSON parse error
        catch (JsonReaderException ex)
        {
            Console.WriteLine($"JSON parse error: {ex.Message}");
        }
        
        // Catch float parse error
        catch (FormatException ex)
        {
            Console.WriteLine($"Float parse error: {ex.Message}");
        }
        
        // Other exception
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        return (null, null);
    }
    
    public static ExchangeData GetBuySellDiffFromJson(string jsonString)
    {
        try
        {
            
            var jsonObject = JObject.Parse(jsonString);
            var rates = jsonObject.SelectToken("rates");
            
            List<ExchangeData> exchanges = 
                rates.AsEnumerable()
                    .Select(rate =>
            {
                return new ExchangeData
                {
                    currencyCode = jsonObject.SelectToken("code").ToString(),
                    effectiveDate = DateTime.Parse(rate.SelectToken("effectiveDate").ToString()),
                    bid = float.Parse(rate.SelectToken("bid").ToString()),
                    ask = float.Parse(rate.SelectToken("ask").ToString())
                };
            })
                    .ToList();
            return exchanges.MaxBy(exchange => exchange.ask - exchange.bid);
        }
        // Catch JSON parse error
        catch (JsonReaderException ex)
        {
            Console.WriteLine($"JSON parse error: {ex.Message}");
        }
        
        // Catch float parse error
        catch (FormatException ex)
        {
            Console.WriteLine($"Float parse error: {ex.Message}");
        }
        
        // Other exception
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        return null;
    }
}
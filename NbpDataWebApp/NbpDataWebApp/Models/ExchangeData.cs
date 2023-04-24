using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NbpDataWebApp.Models;

public class ExchangeData
{ 
    public string currencyCode;
    public float exchangeRate; 
}

public static class ExchangeDataModel
{
    public static ExchangeData GetDataFromJson(string jsonString)
    {
        try
        {
            float exRate = float.Parse(JObject.Parse(jsonString).SelectToken("rates[0].mid").ToString());
            string currCode = JObject.Parse(jsonString).SelectToken("code").ToString();

            var ex = new ExchangeData
            {
                currencyCode = currCode,
                exchangeRate = exRate
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
}
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eCommerce.Models
{
    public class CurrencyService : BackgroundService
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILogger logger;

        public CurrencyService(IMemoryCache memoryCache, 
            ILogger<CurrencyConverter> logger)
        {
            this.memoryCache = memoryCache;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    XDocument xml = XDocument.Load("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange");

                    CurrencyConverter currencyConverter = new CurrencyConverter();
                    currencyConverter.USD = Convert.ToDecimal(xml.Elements("exchange").Elements("currency").FirstOrDefault(x => x.Element("r030").Value == "840").Elements("rate").FirstOrDefault().Value);
                    currencyConverter.EUR = Convert.ToDecimal(xml.Elements("exchange").Elements("currency").FirstOrDefault(x => x.Element("r030").Value == "978").Elements("rate").FirstOrDefault().Value);

                    memoryCache.Set("key_currency", currencyConverter, TimeSpan.FromMinutes(1440));
                    logger.LogInformation("Everything is good!");
                }
                catch (Exception e)
                {
                    logger.LogError(e.InnerException.Message);
                }                
                await Task.Delay(3600000, stoppingToken);
            }
        }
    }
}

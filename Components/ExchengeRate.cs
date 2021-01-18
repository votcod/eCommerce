using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Components
{
    public class ExchengeRate : ViewComponent
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILogger logger;
        public ExchengeRate(
            IMemoryCache memoryCache, 
            ILogger<ExchengeRate> logger)
        {
            this.memoryCache = memoryCache;
            this.logger = logger;
        }
        public IViewComponentResult Invoke()
        {
            if (!memoryCache.TryGetValue("key_currency", out CurrencyConverter modelConvertor))
            {
                throw new Exception("Data retrieval error");
            }
            return View(modelConvertor);
        }
    }
}

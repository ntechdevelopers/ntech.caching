using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ntech.Caching.Context;
using Ntech.Caching.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntech.Caching.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDistributedCache _cache;
        private readonly DataContext _dataContext;

        public long TimeQuery { get; set; }
        public IList<Product> Product { get; set; }

        private MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(5),
            AbsoluteExpiration = DateTime.Now.AddMinutes(60)
        };

        public IndexModel(ILogger<IndexModel> logger, IDistributedCache cache, DataContext dataContext)
        {
            _logger = logger;
            _cache = cache;
            _dataContext = dataContext;
        }

        public void OnGet()
        {
        }

        public IActionResult OnGetProductWithoutCaching()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // the code that you want to measure comes here
            Product = _dataContext.Products.ToList();
           
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
           
            TimeQuery = elapsedMs;
            return Page();
        }

        public async Task<IActionResult> OnGetProductWithMemoryCache()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var cacheKey = "GET_ALL_PRODUCTS";
            List<Product> products = new List<Product>();

            // Get data from cache
            var cachedData = await _cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                // If data found in cache, encode and deserialize cached data
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                products = JsonConvert.DeserializeObject<List<Product>>(cachedDataString);
            }
            else
            {
                // If not found, then fetch data from database
                products = await _dataContext.Products.ToListAsync();

                // serialize data
                var cachedDataString = JsonConvert.SerializeObject(products);
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                // set cache options 
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(2))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                // Add data in cache
                await _cache.SetAsync(cacheKey, newDataToCache, options);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            TimeQuery = elapsedMs;
            return Page();
        }
    }
}

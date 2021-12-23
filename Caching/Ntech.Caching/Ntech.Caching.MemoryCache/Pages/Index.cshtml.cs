using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Ntech.Caching.Context;
using Ntech.Caching.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ntech.Caching.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMemoryCache _cache;
        private readonly DataContext _dataContext;

        public long TimeQuery { get; set; }
        public IList<Product> Product { get; set; }

        private MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(5),
            AbsoluteExpiration = DateTime.Now.AddMinutes(60)
        };

        public IndexModel(ILogger<IndexModel> logger, IMemoryCache cache, DataContext dataContext)
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

        public IActionResult OnGetProductWithMemoryCache()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var cacheKey = "GET_ALL_PRODUCTS";

            // If data found in cache, return cached data
            if (_cache.TryGetValue(cacheKey, out List<Product> products))
            {
                Product = products;
                return Page();
            }

            // If not found, then fetch data from database
            products = _dataContext.Products.ToList();

            // Add data in cache
            //_cache.Set(cacheKey, products);
            _cache.Set(cacheKey, products, cacheOptions);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            TimeQuery = elapsedMs;
            return Page();
        }
    }
}

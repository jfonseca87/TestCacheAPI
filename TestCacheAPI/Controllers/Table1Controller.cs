using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCacheAPI.Context;
using TestCacheAPI.Models;

namespace TestCacheAPI.Controllers
{
    // Without cache strategy
    [ApiController]
    [Route("api/v1/tableOne")]
    public class Table1V1Controller : ControllerBase
    {
        private readonly TestdbContext _db;

        public Table1V1Controller(TestdbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetTableOneList()
        {
            return Ok(await _db.Table1.ToListAsync());    
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetTableOneSingle(int id)
        {
            return Ok(await _db.Table1.FirstOrDefaultAsync(x => x.IdTable == id));
        }
    }

    // With memorycache strategy
    [ApiController]
    [Route("api/v2/tableOne")]
    public class Table1V2Controller : ControllerBase
    {
        private readonly TestdbContext _db;
        private readonly IMemoryCache _memCache;

        public Table1V2Controller(TestdbContext db, IMemoryCache memCache)
        {
            _db = db;
            _memCache = memCache;
        }

        [HttpGet]
        public IActionResult GetTableOneList()
        {
            string cacheKey = "numero";

            if (!_memCache.TryGetValue(cacheKey, out int numero))
            {
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(1)
                };

                _memCache.Set(cacheKey, 1000, cacheOptions);
            }

            return Ok(numero);

            //string key = "tableone";

            //if (!_memCache.TryGetValue(key, out IEnumerable<Table1> tableOneList))
            //{
            //    tableOneList = await _db.Table1.ToListAsync();

            //    var cacheOptions = new MemoryCacheEntryOptions
            //    {
            //        AbsoluteExpiration = DateTime.Now.AddHours(1),
            //        SlidingExpiration = TimeSpan.FromMinutes(30),
            //        Priority = CacheItemPriority.Normal
            //    };

            //    _memCache.Set(key, tableOneList, cacheOptions);
            //}

            //return Ok(tableOneList);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetTableOneSingle(int id)
        {
            return Ok(await _db.Table1.FirstOrDefaultAsync(x => x.IdTable == id));
        }
    }

    // With distributed cache redis strategy
    [ApiController]
    [Route("api/v3/tableOne")]
    public class Table1V3Controller : ControllerBase
    {
        private readonly TestdbContext _db;
        private readonly IDistributedCache _distCache;

        public Table1V3Controller(TestdbContext db, IDistributedCache distCache)
        {
            _db = db;
            _distCache = distCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetTableOneList()
        {
            //List<Table1> lstTableOne;

            //string key = "tableone";
            //var bytesListCache = await _distCache.GetAsync(key);

            //if (bytesListCache != null)
            //{
            //    string stringListCache = Encoding.UTF8.GetString(bytesListCache);
            //    lstTableOne = JsonConvert.DeserializeObject<List<Table1>>(stringListCache);
            //}
            //else
            //{
            //    var optionsCache = new DistributedCacheEntryOptions()
            //                        .SetAbsoluteExpiration(DateTime.Now.AddHours(1));

            //    lstTableOne = await _db.Table1.ToListAsync();
            //    string serializedStringList = JsonConvert.SerializeObject(lstTableOne);
            //    var bytesList = Encoding.UTF8.GetBytes(serializedStringList);

            //    await _distCache.SetAsync(key, bytesList, optionsCache);
            //}

            //return Ok(lstTableOne);

            List<Table1> lstTableOne;

            string key = "tableonev1";
            string stringListCache = await _distCache.GetStringAsync(key);

            if (stringListCache != null)
            {
                lstTableOne = JsonConvert.DeserializeObject<List<Table1>>(stringListCache);
            }
            else
            {
                var optionsCache = new DistributedCacheEntryOptions()
                                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1));

                lstTableOne = await _db.Table1.ToListAsync();
                string serializedStringList = JsonConvert.SerializeObject(lstTableOne);

                await _distCache.SetStringAsync(key, serializedStringList, optionsCache);
            }

            return Ok(lstTableOne);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetTableOneSingle(int id)
        {
            string cacheKey = "test1";
            string value = await _distCache.GetStringAsync(key: cacheKey);

            return Ok(value);
            // return Ok(await _db.Table1.FirstOrDefaultAsync(x => x.IdTable == id));
        }
    }

    // With distributed cache redis strategy in a custom handler
    [ApiController]
    [Route("api/v4/tableOne")]
    public class Table1V4Controller : ControllerBase
    {
        private readonly TestdbContext _db;
        private readonly ICaching _caching;

        public Table1V4Controller(TestdbContext db, ICaching caching)
        {
            _db = db;
            _caching = caching;
        }

        [HttpGet]
        public async Task<IActionResult> GetTableOneList()
        {
            string cacheKey = "tableonev3";
            var list = await _caching.GetObjectValueAsync(cacheKey, Enumerations.CacheDataType.StringData, DateTime.Now.AddHours(1), async () =>
            {
                return await _db.Table1.ToListAsync();
            });

            return Ok(list);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetTableOneSingle(int id)
        {
            string cacheKey = "test1";
            string value = string.Empty;

            return Ok(value);
            // return Ok(await _db.Table1.FirstOrDefaultAsync(x => x.IdTable == id));
        }
    }
}

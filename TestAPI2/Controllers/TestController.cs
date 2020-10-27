using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPI2.Models;

namespace TestAPI2.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public TestController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetCacheValue()
        {
            List<Table1> lstTableOne = new List<Table1>();

            string key = "tableonev1";
            var stringListCache = await _distributedCache.GetStringAsync(key);

            if (stringListCache != null)
            {
                lstTableOne = JsonConvert.DeserializeObject<List<Table1>>(stringListCache);
            }

            return Ok(lstTableOne);

            //List<Table1> lstTableOne = new List<Table1>();

            //string key = "tableonev1";
            //var bytesListCache = await _distributedCache.GetAsync(key);

            //if (bytesListCache != null)
            //{
            //    string stringListCache = Encoding.UTF8.GetString(bytesListCache);
            //    lstTableOne = JsonConvert.DeserializeObject<List<Table1>>(stringListCache);
            //}

            //return Ok(lstTableOne);

            //string cacheKey = "test1";
            //string value = _distributedCache.GetString(key: cacheKey);
            //return Ok(value);
        }
    }
}

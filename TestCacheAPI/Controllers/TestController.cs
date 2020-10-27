using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCacheAPI.Context;
using TestCacheAPI.Models;

namespace TestCacheAPI.Controllers
{
    [ApiController]
    [Route("api/seed")]
    public class TestController : ControllerBase
    {
        private readonly TestdbContext _db;

        public TestController(TestdbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> SeedData()
        {
            try
            {
                List<Table1> table1s = new List<Table1>();

                for (int i = 0; i < 501; i++)
                {
                    _db.Table1.Add(new Table1
                    {
                        Field1 = $"Field 1 {i + 1}",
                        Field2 = $"Field 2 {i + 1}",
                        Field3 = $"Field 3 {i + 1}",
                        Field4 = $"Field 4 {i + 1}",
                        Field5 = $"Field 5 {i + 1}",
                    });

                    //table1s.Add(new Table1
                    //{
                    //    Field1 = $"Field 1 {i + 1}",
                    //    Field2 = $"Field 2 {i + 1}",
                    //    Field3 = $"Field 3 {i + 1}",
                    //    Field4 = $"Field 4 {i + 1}",
                    //    Field5 = $"Field 5 {i + 1}",
                    //});
                }

                // await _db.Table1.AddRangeAsync(table1s);
                bool successfull = await _db.SaveChangesAsync() > 0;

                return Ok(successfull);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

using CoreCSVImport.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CoreCSVImport.Lib.Mapping;
using System.IO;
using System.Collections.Generic;
using System;

namespace CoreCSVImport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICvsFileHelper _cvsFileHelper;
        public CSVController(ApplicationDbContext context, ICvsFileHelper cvsFileHelper
            )
        {
            _context = context;
            _cvsFileHelper = cvsFileHelper;
        }

        // [HttpPost()]
    [Route("[action]/{formFile?}/{category?}")]
        public async Task<IActionResult> LoadCSV(IFormFile formFile, int category)
        {
            List<Object> returnValue = new List<object>();
            try{
                if (formFile?.Length > 0)
                {
                    // Full path to file in temp location
                    var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    returnValue = await _cvsFileHelper.MapCSVFile(filePath,category);

                }
                else
                {
                    return BadRequest("CSV file is required.");

                }
            }
            catch(Exception e){
                return BadRequest(e);
            }
            return Ok(returnValue);
        }
    }
}

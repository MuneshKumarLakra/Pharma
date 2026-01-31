using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PharmaCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileRepository _fileRepository;
        public FileController(ILogger<FileController> logger, IFileRepository fileRepository)
        {
            _logger = logger;
            _fileRepository = fileRepository;
        }

        [HttpPost]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> PostAsync([FromBody] DrugDetails drugDetails)
        {
            _logger.LogInformation("writing to text file data {0} {1}", drugDetails.DrugName, drugDetails.DrugExpiredOn, drugDetails.LotNo);
            _logger.LogTrace("writing to text file data {0} {1}", drugDetails.DrugName, drugDetails.DrugExpiredOn, drugDetails.LotNo);

            string filePath = @"c:\FileData\Filedata.txt";
            var result = await _fileRepository.WriteTextAsync(filePath, drugDetails);
            if (result.Length > 0)
            {
                _logger.LogInformation("data written to file sucessfully");
                _logger.LogTrace("data written to file sucessfully");
                return Ok(result);
            }

            return BadRequest();
        }
    }
}

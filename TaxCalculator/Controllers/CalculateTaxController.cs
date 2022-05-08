using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TaxCalculator.Repository.IRepository;

namespace TaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateTaxController : ControllerBase
    {
        private readonly ICalculateTaxRepository _ctRepo;
        private readonly ILogger<CalculateTaxController> _logger;

        public CalculateTaxController(ICalculateTaxRepository ctRepo,ILogger<CalculateTaxController> logger)
        {
            _ctRepo = ctRepo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTax(string municipality,string taxDate)
        {
            _logger.LogInformation("GetTax endpoint call started");

            if (municipality == null || taxDate == null)
            {
                return BadRequest(ModelState);
            }
            if (!DateTime.TryParse(taxDate, out _))
            {
                return BadRequest("Invalid taxDate format.");
            }
            if (!_ctRepo.MunicipalityExists(municipality))
            {
                return NotFound("Municipality does not exist.");
            }

            float tax = _ctRepo.GetMunicipalityTax(municipality, Convert.ToDateTime(taxDate));
            _logger.LogInformation("GetTax endpoint call ended");
            return Ok(tax);
        }
    }
}

using System;
using System.Collections.Generic;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly IDutchTreatRepository _dutchTreatRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IDutchTreatRepository dutchTreatRepository, ILogger<ProductsController> logger)
        {
            _dutchTreatRepository = dutchTreatRepository;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)] // To add 
        [ProducesResponseType(400)]
        // IActionResult allow you to send data with status messages
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            try
            {
                return Ok(_dutchTreatRepository.GetAllProducts());
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get products: {e}");
               return BadRequest("Failed to get products");
            }
           
        }
    }
}
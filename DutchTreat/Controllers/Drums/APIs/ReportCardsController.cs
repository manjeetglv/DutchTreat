using System;
using Drums.Data.Entities;
using Drums.Data.Repositories;
using DutchTreat.Data.Entities;
using DutchTreat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers.Drums.APIs
{
    [Route("/api/[Controller]")]
    [ApiController]
    public class ReportCardsController: ControllerBase
    {
        private readonly IReportCardRepository _reportCardRepository;
        private readonly ILogger<ReportCardsController> _logger;

        public ReportCardsController(IReportCardRepository reportCardRepository, ILogger<ReportCardsController> logger)
        {
            _reportCardRepository = reportCardRepository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreateReportCard(ReportCard reportCard)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    _reportCardRepository.AddReportCard(reportCard);

                    return Created($"/api/[Controller]/{reportCard.Id}", reportCard);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to Report Card: {e}");
            }
            
            return BadRequest("Failed to save Report Card");
        }
        
        [HttpGet("{reportCardId}")]
        public IActionResult Get(int reportCardId)
        {
           
            try
            {
                ReportCard reportCard = _reportCardRepository.GetReportCardById(reportCardId);
                if (reportCard == null) return NotFound();
                // IEnumerable<OrderItemViewModel> orderItemViewModel = _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(reportCard.Items);
                // return Ok(orderItemViewModel);
                return Ok(reportCard);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to fetch Items: {e}");
                return BadRequest();
            }
        }
    }
}
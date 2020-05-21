using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("/api/orders/{orderId}/items")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController: ControllerBase
    {
        private readonly IDutchTreatRepository _dutchTreatRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IDutchTreatRepository dutchTreatRepository, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            _dutchTreatRepository = dutchTreatRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<OrderItem> GetOrderItems(int orderId)
        {
            try
            {
                Order order = _dutchTreatRepository.GetOrderById(User.Identity.Name, orderId);
                if (order == null) return NotFound();
                IEnumerable<OrderItemViewModel> orderItemViewModel = _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items);
                return Ok(orderItemViewModel);
            }
            catch (Exception e)
            {
               _logger.LogError($"Failed to fetch Items: {e}");
               return BadRequest();
            }
        }

        [HttpGet("{itemId}")]

        public ActionResult<OrderItem> GetOrderItem(int orderId, int itemId)
        {
            try
            {
                Order order = _dutchTreatRepository.GetOrderById(User.Identity.Name, orderId);
                if (order == null) return NotFound();
                
                OrderItem orderItem = order.Items.FirstOrDefault(item => item.Id == itemId);
                if (orderItem == null) return NotFound();
                
                OrderItemViewModel orderItemViewModel = _mapper.Map<OrderItem, OrderItemViewModel>(orderItem);
                
                return Ok(orderItemViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to fetch Items: {e}");
                return BadRequest();
            }
        }
    }
}
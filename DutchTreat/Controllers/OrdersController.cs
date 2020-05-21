using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")] // http:testapp/api/orders
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IDutchTreatRepository _dutchTreatRepository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IDutchTreatRepository dutchTreatRepository, ILogger<OrdersController> logger, IMapper mapper, UserManager<StoreUser> userManager)
        {
            _dutchTreatRepository = dutchTreatRepository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult<Order> GetAllOrders(bool includeItems = true)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_dutchTreatRepository.GetAllOrdersByUser(User.Identity.Name, includeItems)));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get Orders: {e}");
                return BadRequest("Failed to get Orders");
            }
        }
        
        [HttpGet("{orderId:int}")]
        public ActionResult<Order> GetOrderById(int orderId)
        {
            try
            {
                Order order = _dutchTreatRepository.GetOrderById(User.Identity.Name, orderId);
                if (order == null) return NotFound();
                else return Ok(_mapper.Map<Order, OrderViewModel>(order));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get Order: {e}");
                return BadRequest("Failed to get Order");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Order>> SaveOrder([FromBody]OrderViewModel orderView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Order order = _mapper.Map<OrderViewModel, Order>(orderView);
                    order.OrderDate = orderView.OrderDate == DateTime.MinValue ? DateTime.Now : orderView.OrderDate;

                    // We are fetch the user again because the "User.Identity.Name" is not attached to dbcontext
                    StoreUser currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    order.User = currentUser;
                    
                    _dutchTreatRepository.AddOrder(order);
                    
                    if (_dutchTreatRepository.SaveAll())
                    {
                        OrderViewModel updatedOderView = _mapper.Map<Order, OrderViewModel>(order);
                        return Created($"/api/order/{order.Id}", updatedOderView);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to save Order: {e}");
            }
            
            return BadRequest("Failed to save Order");
        }

       
    }
}
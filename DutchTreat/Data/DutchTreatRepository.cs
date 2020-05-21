using System;
using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data
{
    
    public class DutchTreatRepository : IDutchTreatRepository
    {
        private readonly DutchTreatContext _dutchTreatContext;
        private readonly ILogger<DutchTreatRepository> _logger;

        public DutchTreatRepository(DutchTreatContext dutchTreatContext, ILogger<DutchTreatRepository> logger)
        {
            _dutchTreatContext = dutchTreatContext;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            { 
                _logger.LogInformation("GetAllProducts is called");
                return _dutchTreatContext.Products.OrderBy(product => product.Title).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get all products {e}");
                return null;
            }
            
        }

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            return _dutchTreatContext.Products.Where(product => product.Category == category).ToList(); 
        }

        public bool SaveAll()
        {
            return _dutchTreatContext.SaveChanges() > 0;
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _dutchTreatContext.Orders
                    .Where(order => order.User.UserName == username)
                    .Include(order => order.Items)
                    .ThenInclude(item => item.Product)
                    .ToList();    
            }
            else
            {
                return _dutchTreatContext.Orders.Where(order => order.User.UserName == username).ToList();
            }
            
        }

        public Order GetOrderById(string username, int orderId)
        {
            return _dutchTreatContext.Orders
                .Include(order => order.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefault(order => order.Id == orderId && order.User.UserName == username);
        }

        public void AddOrder(Order order)
        {
            // Convert new product to lookup of product
            foreach (OrderItem orderItem in order.Items)
            {
                orderItem.Product = _dutchTreatContext.Products.Find(orderItem.Product.Id);
            }
            AddEntity(order);
        }

        public void AddEntity(object order)
        {
            _dutchTreatContext.Add(order);
        }
        
    }
}
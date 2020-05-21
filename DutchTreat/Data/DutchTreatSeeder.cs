using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DutchTreat.Data
{
    public class DutchTreatSeeder
    {
        private readonly DutchTreatContext _dutchTreatContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<StoreUser> _userManager;

        public DutchTreatSeeder(DutchTreatContext dutchTreatContext, IWebHostEnvironment webHostEnvironment, UserManager<StoreUser> userManager)
        {
            _dutchTreatContext = dutchTreatContext;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _dutchTreatContext.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("manjeet@webilize.com");
            if (user == null)
            {
                 
                user = new StoreUser
                {
                    FirstName = "Manjeet",
                    LastName = "Lama",
                    Email = "manjeet@webilize.com",
                    UserName = "manjeet@webilize.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in Seeder");
                }
            }

            if (_dutchTreatContext.Products.Any()) return;
            //Need to creat sample data
            string artJsonFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Data/art.json");
            string artJsonFileData = File.ReadAllText(artJsonFilePath);
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(artJsonFileData);
            _dutchTreatContext.Products.AddRange(products);

            Order order = _dutchTreatContext.Orders.FirstOrDefault(o => o.Id == 1);
            if (order != null)
            {
                order.User = user;
                order.Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Product = products.First(),
                        Quantity = 5,
                        UnitPrice = products.First().Price
                    }
                };
            }


            _dutchTreatContext.SaveChanges();
        }

    }
}
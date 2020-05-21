using System;
using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Models;
using DutchTreat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchTreatRepository _dutchTreatRepository;
        
        public AppController(IMailService mailService, IDutchTreatRepository dutchTreatRepository)
        {
            _mailService = mailService;
            _dutchTreatRepository = dutchTreatRepository;
        }
        
        public IActionResult Index()
        {
            IEnumerable<Product> products = _dutchTreatRepository.GetAllProducts();
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            // throw new InvalidOperationException("I will not work");
            return View();
        }
        
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            Console.WriteLine("I am here1"+model.ToString());
            if (ModelState.IsValid)
            {
                // Sent the email
                _mailService.SendMessage(model.Email, "Query Acknowledged", "Will reach you back in 2 business days");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            // throw new InvalidOperationException("I will not work");
            return View();
        }
        
        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }
        
        public IActionResult Shop()
        {
            //Before angular
            // IEnumerable<Product> products = _dutchTreatRepository.GetAllProducts();
            // return View(products);
            // After angular
            return View();
        }
    }
}
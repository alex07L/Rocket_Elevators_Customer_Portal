using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rocket_Elevators_Customer_Portal.Models;

namespace Rocket_Elevators_Customer_Portal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


   
        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.batteryStatus = new int[] { 0, 0, 0 };
            ViewBag.columnStatus = new int[] { 0, 0, 0 };
            ViewBag.elevatorStatus = new int[]{0,0,0};
            var response = await Program.queryAsync("query addressbyCustomer($email: String!){addressbyCustomer(email: $email){id street suite city postalCode country}}", new { email = User.Identity.Name });
            ViewBag.address = response["addressbyCustomer"];
            var cx = await Program.queryAsync("query cxbyCustomer($email: String!){cxbyCustomer(email: $email){build{id techName} battery{id build_id status} column{id battery_id status} elevator{id column_id status}}}", new { email = User.Identity.Name });
            ViewBag.customer = cx["cxbyCustomer"];
            return View();
        }

        public async Task<IActionResult> AddressAsync(String id)
        {
            if(id != null)
            {
                var response = await Program.queryAsync("query address($id: Int!){address(id: $id){id street suite city postalCode country}}", new { id = id });
                ViewBag.address = response["address"];
                return View();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> InterventionAsync()
        {
            Console.WriteLine("----------");
            Console.WriteLine(User.Identity.Name);
            Console.WriteLine("---------------");

            var response = await Program.queryAsync("query cxbyCustomer($email: String!){cxbyCustomer(email: $email){build{id} battery{id build_id} column{id battery_id} elevator{id column_id}}}", new { email = User.Identity.Name });
            ViewBag.customer = response["cxbyCustomer"];
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> NeedInterventionAsync(String building,String battery,String column, String elevator, String description)
        {
            
            
            var response = await Program.queryAsync("mutation addIntevention($email: String!,$build: Int!, $battery: Int!,$column: Int!, $elevator: Int!, $description: String!){ addIntevention(customer: $email, build: $build, battery: $battery, column: $column, elevator: $elevator, description: $description){id}}", new { email = User.Identity.Name, build = int.Parse(building), battery = int.Parse(battery), column = int.Parse(column), elevator = int.Parse(elevator), description = description });
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> ModifyAddressAsync(String id, String street, String suite, String city, String postalcode, String country)
        {
            if(suite == null)
            {
                suite = "";
            }
            var response = await Program.queryAsync("mutation UpdateAddress($id: Int!,$street: String!, $suite: String!,$city: String!, $postalCode: String!, $country: String!){ UpdateAddress(id: $id, street: $street, suite: $suite, city: $city, postalCode: $postalCode, country: $country){id}}", new { id =id, street = street, suite = suite, city = city, postalCode = postalcode, country = country});
            return RedirectToAction("Index");
        }

        

    }
}

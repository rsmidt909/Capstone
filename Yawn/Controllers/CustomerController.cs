using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yawn.Data;

namespace Yawn.Controllers
{
    public class CustomerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Customer
        public ActionResult Index()
        {
            Customer customer = GetLoggedInUser();
            return View(customer);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            Customer customer = new Customer();
            return View(customer);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FirstName","LastName","StreetAddress","City","State","ZipCode","PhoneNumber","Filters","NumberOfSystems","ApplicationId")]Customer customer)
        {
            try
            {
                // TODO: Add insert logic here
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                customer.ApplicationId = userId;
                _context.Add(customer);
                _context.SaveChanges();


                return RedirectToAction("Index", "Customer");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            Customer customer = GetLoggedInUser();
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
           
                try
                {
                    // TODO: Add update logic here

                    Customer customer = _context.Customers.Where(c => c.id == id).FirstOrDefault();
                _context.Update(customer);
                _context.SaveChanges();

                return RedirectToAction("Index","Customer");
                }
                catch
                {
                    return View();
                }
            
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Service()
        {
            Customer customer = GetLoggedInUser();
            return View(customer);
        }
        public ActionResult Checks()
        {
            Customer customer = GetLoggedInUser();
            return View(customer);
        }

        public Customer GetLoggedInUser()
        {
            
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Customer customer = _context.Customers.Where(c => c.ApplicationId == currentUserId).FirstOrDefault();
            return customer;
        }

        public ActionResult SubmitServiceForm(string[] memo)
        {
            Customer customer = GetLoggedInUser();
            ServiceCalls serviceCalls = new ServiceCalls();
            serviceCalls.FirstName = customer.FirstName;
            serviceCalls.LastName = customer.LastName;
            serviceCalls.Phone = customer.PhoneNumber;
            serviceCalls.Address = customer.StreetAddress;
            serviceCalls.Memo = memo[0];                                 
            serviceCalls.ApplicationId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(serviceCalls);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult SubmitCheckForm(string[] memo)
        {
            Customer customer = GetLoggedInUser();
            Checks checks = new Checks();
            checks.FirstName = customer.FirstName;
            checks.LastName = customer.LastName;
            checks.Phone = customer.PhoneNumber;
            checks.Address = customer.StreetAddress;
            checks.Memo = memo[0];
            checks.ApplicationId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(checks);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}
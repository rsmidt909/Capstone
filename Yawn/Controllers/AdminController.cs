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
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Admin
        public ActionResult Index()
        {
            Admin admin = GetLoggedInUser();

            return View(admin);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {

            Admin admin = GetLoggedInUser();
            return View(admin);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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

        public ActionResult Contacts()
        {
            var customer = _context.Customers.ToList();

            return View(customer);
        }
        public ActionResult CallList()
        {
            TotalCalls calls = new TotalCalls();
            calls.ServiceCalls = _context.Services.ToList();
            calls.Checks = _context.Check.ToList();
            calls.Customer = _context.Customers.ToList();

            return View(calls);
        }

        public Admin GetLoggedInUser()
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Admin admin = _context.Admins.Where(c => c.ApplicationId == currentUserId).FirstOrDefault();
            return admin;
        }
    }
}
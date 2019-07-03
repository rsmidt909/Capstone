using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using System.Text.Encodings.Web;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yawn.Data;
using Application;

namespace Yawn.Controllers
{
    public class CustomerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        //Collection of ChatBot Messages

        //static Dictionary<string, string> lexSessionData = new Dictionary<string, string>();
        private readonly IAWSLexService awsLexSvc;
        private ISession userHttpSession;
        private Dictionary<string, string> lexSessionData;
        private List<ChatBotMessage> botMessages;
        private string botMsgKey = "ChatBotMessages",
                       botAtrribsKey = "LexSessionData",
                       userSessionID = String.Empty;
        public CustomerController(ApplicationDbContext context,IAWSLexService awsLexService)
        {
            _context = context;
            awsLexSvc = awsLexService;

        }
        // GET: Customer
        public ActionResult Index(List<ChatBotMessage> messages)
        {
            CustomerChat customerChat = new CustomerChat();
            customerChat.Customer = GetLoggedInUser();
            customerChat.ChatBotMessage = messages;
            return View(customerChat);
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
            serviceCalls.NumberOfSystems = customer.NumberOfSystems;
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
            checks.Filters = customer.Filters;
            checks.NumberOfSystems = customer.NumberOfSystems;
            checks.ApplicationId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(checks);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        //public IActionResult ClearBot()
        //{
        //    userHttpSession = HttpContext.Session;
        //    userHttpSession.Clear();
        //    botMessages = new List<ChatBotMessage>();
        //    lexSessionData = new Dictionary<string, string>();
        //    userHttpSession.Set<List<ChatBotMessage>>(botMsgKey, botMessages);
        //    userHttpSession.Set<Dictionary<string, string>>(botAtrribsKey, lexSessionData);

        //    awsLexSvc.Dispose();
        //    return View("Index", botMessages);
        //}
        [HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetChatMessage(string userMessage)

        {
            //Get user session and chat info
            userHttpSession = HttpContext.Session;
            userSessionID = userHttpSession.Id;
            botMessages = userHttpSession.Get<List<ChatBotMessage>>(botMsgKey) ?? new List<ChatBotMessage>();
            lexSessionData = userHttpSession.Get<Dictionary<string, string>>(botAtrribsKey) ?? new Dictionary<string, string>();

            //No message was provided, return to current view
            if (String.IsNullOrEmpty(userMessage)) return View("Index", botMessages);

            //A Valid Message exists, Add to page and allow Lex to process
            botMessages.Add(new ChatBotMessage()
            { MsgType = MessageType.UserMessage, ChatMessage = userMessage });

            await postUserData(botMessages);

            //Call Amazon Lex with Text, capture response
            var lexResponse = await awsLexSvc.SendTextMsgToLex(userMessage, lexSessionData, userSessionID);

            lexSessionData = lexResponse.SessionAttributes;
            botMessages.Add(new ChatBotMessage()
            { MsgType = MessageType.LexMessage, ChatMessage = lexResponse.Message });

            //Add updated botMessages and lexSessionData object to Session
            userHttpSession.Set<List<ChatBotMessage>>(botMsgKey, botMessages);
            userHttpSession.Set<Dictionary<string, string>>(botAtrribsKey, lexSessionData);

            return View("Index", botMessages);
        }

        public async Task<IActionResult> postUserData(List<ChatBotMessage> messages)
        {
            //testing
            return await Task.Run(() => Index(messages));
        }

        

    }
}
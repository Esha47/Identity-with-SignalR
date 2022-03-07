using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR_with_identity.Data;
using SignalR_with_identity.Hubs;
using SignalR_with_identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SignalR_with_identity.Controllers
{
    public class ConsumeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ConsumeController(ApplicationDbContext context)
        {
            _context = context;
        }
        /* private readonly IHubContext<UserInfoHub> _notificationHubContext;
         public ConsumeController(IHubContext<UserInfoHub> notificationHubContext)
         {
             _notificationHubContext = notificationHubContext;
         }*/
        //object of httpclient
        HttpClient client = new HttpClient();
        //this list will be get from api
        public IActionResult GetAllUsers()
        {
            List<UserApp> userList = new List<UserApp>();
            //base address is the address of web api local host
            client.BaseAddress = new Uri("https://localhost:44317");
            var response = client.GetAsync("/api/Auth/allUsers");
            //use async with wait
            response.Wait();
            //store the result of response in test variable
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadFromJsonAsync<List<UserApp>>();
                display.Wait();
                userList = display.Result;
            }

            return View(userList);
        }
        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterUser(UserApp user)
        {
            var userd = user;
            client.BaseAddress = new Uri("https://localhost:44317");
            var postUser = client.PostAsJsonAsync<UserApp>("/api/Auth/RegisterUser", user);
            postUser.Wait();
            var test = postUser.Result;
            if (test.IsSuccessStatusCode)
            {
                /* string url = string.Format("/Consume/SignalR?name={0}&email={1}&password={2}",
                    userd.Username, userd.Email, userd.Password
                  );*/
                TempData["username"] = userd.Username;
                TempData["email"] = userd.Email;
                TempData["password"] = userd.Password;

                string url = string.Format("/Consume/SignalR");
                return Redirect(url);
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult updateUser(string email)
        {
            var user = _context.Users.FirstOrDefault(s => s.Email == email);
            UserApp u = new UserApp();
            u.Username = user.UserName;
            u.Email = user.Email;
            return View(u);
        }
        [HttpPost]
        public IActionResult updateUser(UserApp user)
        {
            var userinfo = _context.Users.FirstOrDefault(s => s.Email == user.Email);
            userinfo.UserName = user.Username;
            userinfo.Email = user.Email;
            _context.Update(userinfo);
            _context.SaveChanges();

            TempData["username"] = userinfo.UserName;
            TempData["email"] = userinfo.Email;

            string url = string.Format("/Consume/SignalR");
            return Redirect(url);
        }
        /* public IActionResult SignalR(string name, string email, string password)*/
        public IActionResult SignalR()
        {
            UserApp u = new UserApp();
           u.Username =Convert.ToString(TempData["username"]);
           u.Email = Convert.ToString(TempData["email"]);
           u.Password= Convert.ToString(TempData["password"]);
            /* var obj = new
             {
                 name,
                 email,
                 password
             };
             UserApp u = new UserApp();
             u.Email = obj.email;
             u.Password = obj.password;
             u.Username = obj.name;
 */
            return View(u);
        }
        /*   public IActionResult SignalR()
           {
               return View();
           }*/
        /*  [HttpPost]
          public async Task<IActionResult> Index()
          {
              await _notificationHubContext.Clients.All.SendAsync("SendUserInfo", "sdg");
              return View();
          }*/
    }
}


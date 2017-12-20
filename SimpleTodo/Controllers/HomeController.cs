using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleTodo.Models;

namespace SimpleTodo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public List<UserModel> UserList { get; set; }
        public HomeController(IOptions<List<UserModel>> options)
        {
            UserList = options.Value;
        }
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 时间线
        /// </summary>
        /// <returns></returns>
        public IActionResult TimeLine()
        {
            TodoList todo = new TodoList
            {
                DataList = new List<TodoModel>()
            };
            using (var context=new MyDbContext(User.Identity.Name))
            {
                todo.DataList = context.TodoList.OrderByDescending(p => p.RecID).Take(10).ToList();
            }
            return View(todo);
        }
        /// <summary>
        /// 每日bing
        /// </summary>
        /// <returns></returns>
        public IActionResult MainPage()
        {
            return View();
        }
        #region 登录登出
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (UserList.Any(p => p.UserName == username && p.Password == password))
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, username));

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                    , new ClaimsPrincipal(identity)
                    , new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(15))
                    }
                    ).Wait();

                return Content("true");
            }
            return Content("false");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Login");
        }
        #endregion
        public IActionResult AddTodo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTodo(string content)
        {
            try
            {
                using (var context = new MyDbContext(User.Identity.Name))
                {
                    TodoModel model = new TodoModel
                    {
                        UserName = User.Identity.Name,
                        Content = content,
                        CreateTime = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString()
                    };
                    context.TodoList.Add(model);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Content(ex.InnerExceptionMessage());
            }
            return Content("true");
        }

    }
}

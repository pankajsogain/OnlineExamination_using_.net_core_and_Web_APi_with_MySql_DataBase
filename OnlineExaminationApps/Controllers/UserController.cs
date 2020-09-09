using System;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationApps.Handler;
using OnlineExaminationApps.Models;

namespace OnlineExaminationApps.Controllers
{
    public class UserController : Controller
    {
        private readonly ApiHandler apiHandler;
        public UserController()
        {
            apiHandler = new ApiHandler();
        }

        // GET: UserController/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: UserController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            try
            {
                if(apiHandler.ValidateEmail(user.Email).Result)
                {
                   var responsestring=apiHandler.GetLoginTokenAsync(user).Result;
                    if(!string.IsNullOrEmpty(responsestring))
                    {
                        var response=responsestring.Split(":");
                        Session.UserToken = response[1];
                        Session.Role = response[0];
                        Session.CustId =Convert.ToInt32(response[2]);
                        Session.UserName = user.Email;
                        
                        if(Session.Role=="Admin")
                          return RedirectToAction("Index", "Admin");
                        else
                            return RedirectToAction("Index", "Candidate");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please enter correct email and password");
                        return View(user);
                    }

                }
                else
                {
                    ModelState.AddModelError("","Please enter correct email and password");
                    return View(user);
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }
        public IActionResult Logoff()
        {
            Session.Role = string.Empty;
            Session.UserName = string.Empty;
            Session.UserToken = string.Empty;
            Session.CustId = 0;
            Session.Message = new Message();
            return RedirectToAction("Login","User");

        }
    }
}

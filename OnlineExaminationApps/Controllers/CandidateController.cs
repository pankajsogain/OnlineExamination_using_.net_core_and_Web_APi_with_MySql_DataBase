using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationApps.Handler;
using OnlineExaminationApps.Models;

namespace OnlineExaminationApps.Controllers
{
    public class CandidateController : Controller
    {
        private readonly IApiHandler _apiHandler;
        public CandidateController(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }
        public async Task<ActionResult> Index()
        {
            var simtests = await _apiHandler.GetAllSimTest();
            
            return View(simtests);
        }
        public async Task<IActionResult> Test(int id)
        {
            try
            {
                var vmQuestions = await _apiHandler.GetQuetionsBySimTestId(id);
                return View(vmQuestions);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Session.Message = new Message() { CssClassName = "alert-error", Title = "Error!", Messages = "Operation Failed." };
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Test(VmQuestions vmQuestions)
        {
            vmQuestions.CustId = Session.CustId;
            var result=await _apiHandler.CheckResult(vmQuestions);          
            if(result!=null)
            {
              return  View("Result",result);
            }
           return View();            
        }
        public IActionResult Result(Assignedtest assignedtest)
        {                          
           return View(assignedtest);            
            
        }

    }
}

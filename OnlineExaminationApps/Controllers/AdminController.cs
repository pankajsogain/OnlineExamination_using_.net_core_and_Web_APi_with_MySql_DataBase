using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationApps.Handler;
using OnlineExaminationApps.Models;

namespace OnlineExaminationApps.Controllers
{    
    public class AdminController : Controller
    {
        private readonly IApiHandler _apiHandler;
        public AdminController(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }
        public async Task<ActionResult> Index()
        {
            var simtests = await _apiHandler.GetAllSimTest();
            return View(simtests);
        }

        // GET: AdminController/Create
        public async Task<ActionResult> Create()
        {
            Registration registration = new Registration();
            IList<Countries> countries = await _apiHandler.GetCountries();
            @ViewBag.ListofCountry = countries;
            @ViewBag.ListofStates = new List<States>();
            @ViewBag.ListofCities = new List<Cities>();
            return View(registration);
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Registration registration)
        {
            try
            {
                IList<Countries> countrie = await _apiHandler.GetCountries();
                @ViewBag.ListofCountry = countrie;
                @ViewBag.ListofStates = new List<States>();
                @ViewBag.ListofCities = new List<Cities>();

                if (_apiHandler.ValidateEmail(registration.EmailId).Result)
                {
                    ModelState.AddModelError("EmailId", "Email Id is Already Exists");
                }
                if (!ModelState.IsValid)
                {
                    if (registration.Country == 0)
                    {
                        ModelState.AddModelError("Country", "Country is required");
                    }
                    if (registration.Country != 0)
                    {
                        @ViewBag.ListofStates = await _apiHandler.GetStatesByCountryId(registration.Country);
                    }
                    else
                    {
                        @ViewBag.ListofStates = new List<States>();
                    }

                    if (registration.State != 0)
                    {
                        @ViewBag.ListofCities = await _apiHandler.GetCitiesByStateId(registration.State);
                    }
                    else
                    {
                        @ViewBag.ListofCities = new List<Cities>();
                    }

                    return View(registration);
                }

                var response = await _apiHandler.Registration(registration);
                
                Session.Message = new Message() { CssClassName = "alert-sucess", Title = "Success!", Messages = "Candidate successfully created." };
                return View();
            }
            catch
            {
                Session.Message = new Message() { CssClassName = "alert-error", Title = "Error!", Messages = "Operation Failed." };
                return View();
            }
        }
        public async Task<JsonResult> GetStatesByCountryId(int countryId)
        {
            IList<States> states = await _apiHandler.GetStatesByCountryId(countryId);
            return Json(states);
        }
        public async Task<JsonResult> GetCitiesByStateId(int stateId)
        {
            IList<Cities> cities = await _apiHandler.GetCitiesByStateId(stateId);
            return Json(cities);
        }

        public ActionResult CreateTest()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateTest(SimTest simtest)
        {
            try
            {
                await _apiHandler.CreateTestAsync(simtest);
                Session.Message = new Message() { CssClassName = "alert-sucess", Title = "Success!", Messages = "Test successfully created." };
                return View();
            }
            catch
            {
                Session.Message = new Message() { CssClassName = "alert-error", Title = "Error!", Messages = "Operation Failed." };
                return View();
            }
        }
        public async Task<ActionResult> EditTest(int id)
        {
            SimTest simTest = await _apiHandler.GetSimTest(id);
            return View(simTest);
        }
        [HttpPost]
        public async Task<ActionResult> EditTest(SimTest simTest)
        {
            try
            {
                var response = await _apiHandler.UpdateSimTest(simTest);
                Session.Message = new Message() { CssClassName = "alert-sucess", Title = "Success!", Messages = "Updated." };
                return View();
            }
            catch
            {
                Session.Message = new Message() { CssClassName = "alert-error", Title = "Error!", Messages = "Operation Failed." };
                return View();
            }
        }

        public async Task<ActionResult> DeleteTest(int id)
        {
            try
            {
                var response = await _apiHandler.DeleteSimTest(id);
                Session.Message = new Message() { CssClassName = "alert-sucess", Title = "Success!", Messages = "Deleted." };
                return RedirectToAction("Index");
            }
            catch
            {
                Session.Message = new Message() { CssClassName = "alert-error", Title = "Error!", Messages = "Operation Failed." };
                return View();
            }
        }
        public async Task<ActionResult> AddQues(int id)
        {
            try
            {
                var vmQuestions = await _apiHandler.GetQuetionsBySimTestId(id);
                return View(vmQuestions);
            }
            catch
            {
                Session.Message = new Message() { CssClassName = "alert-error", Title = "Error!", Messages = "Operation Failed." };
                return View();
            }
        }
        public IActionResult AddQuestion(int id)
        {
            Questionwithchoice questionwithchoice = new Questionwithchoice();
            questionwithchoice.Questions = new Questions();
            questionwithchoice.Questions.TestId = id;
            return View(questionwithchoice);
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestion(Questionwithchoice questionwithchoice)
        {
            await _apiHandler.AddQuestion(questionwithchoice);
            return RedirectToAction("AddQues", new { id = questionwithchoice.Questions.TestId });
        }

        public async Task<IActionResult> AssignTest(int id)
        {
            Assignedtest assignedtest = new Assignedtest();
            assignedtest.TestId = id;
            @ViewBag.ListofCustomers = await _apiHandler.GetCustomers();
            @ViewBag.ListofSimTest =_apiHandler.GetSimTest(id).Result.TestName;
            return View(assignedtest);
        }
        [HttpPost]
        public  async Task<IActionResult> AssignTest(Assignedtest assignedtest)
        {
            try
            {
                assignedtest.Id = 0;
                var response = _apiHandler.AssignTest(assignedtest);
                Session.Message = new Message() { CssClassName = "alert-sucess", Title = "Success!", Messages = "Test Assigned." };
                return RedirectToAction("Index");
            }
            catch
            {
                Session.Message = new Message() { CssClassName = "alert-error", Title = "Error!", Messages = "Operation Failed." };
                @ViewBag.ListofCustomers = await _apiHandler.GetCustomers();
                @ViewBag.ListofSimTest = _apiHandler.GetSimTest(assignedtest.TestId).Result.TestName;
                return View(assignedtest);
            }
        }

        
    }
}
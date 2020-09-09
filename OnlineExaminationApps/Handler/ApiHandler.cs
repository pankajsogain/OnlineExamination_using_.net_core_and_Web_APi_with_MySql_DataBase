using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BRQ.Application.Common.Helpers;
using OnlineExaminationApps.Models;

namespace OnlineExaminationApps.Handler
{
    public class ApiHandler: IApiHandler
    {
        private readonly HttpClient _httpClient;
        public ApiHandler()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44370/api/");
            _httpClient.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer", Session.UserToken);
        }

        public async Task<IList<SimTest>> GetAllSimTest()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Admin/GetAllSimTest");
            IList<SimTest> simtests = await Utilities.GetResponseContent<List<SimTest>>(response);
            return simtests;
        }
        public async Task<HttpResponseMessage> DeleteSimTest(int simtestid)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Admin/DeleteSimTest?simtestid={simtestid}");
            return response;            
        }
        public async Task<SimTest> GetSimTest(int simtestid)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Admin/GetSimTest?simtestid={simtestid}");
            SimTest simtests = await Utilities.GetResponseContent<SimTest>(response);
            return simtests;
        }

        public async Task<Assignedtest> CheckResult(VmQuestions vmQuestions)
        {
            var content = Utilities.GetRequestContent(vmQuestions);
            HttpResponseMessage response = await _httpClient.PostAsync($"Admin/CheckResult", content);
            var assignedtest = await Utilities.GetResponseContent<Assignedtest>(response);
            return assignedtest;
        }

        public async Task<HttpResponseMessage> UpdateSimTest(SimTest simtest)
        {
            var content = Utilities.GetRequestContent(simtest);
            HttpResponseMessage response = await _httpClient.PostAsync($"Admin/UpdateSimTest", content);
            return response;
        }

        public async Task<IList<Countries>> GetCountries()
        {             
            HttpResponseMessage response =await _httpClient.GetAsync("Utility/GetCountries");
            List<Countries> countries = await Utilities.GetResponseContent<List<Countries>>(response);
          return countries;
        }
        public async Task<IList<States>> GetStatesByCountryId(int countryId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Utility/GetStatesByCountryId?countryId={countryId}");
            List<States> states = await Utilities.GetResponseContent<List<States>>(response);
            return states;
        }
        public async Task<IList<Cities>> GetCitiesByStateId(int stateId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Utility/GetCitiesByStateId?stateId={stateId}");
            List<Cities> cities = await Utilities.GetResponseContent<List<Cities>>(response);
            return cities;
        }
        public async Task<string> Registration(Registration registration)
        {
            var content = Utilities.GetRequestContent(registration);
            HttpResponseMessage response = await _httpClient.PostAsync($"Admin/Registration",content);
            return response.RequestMessage.ToString();
        }
        public async Task<bool> ValidateEmail(string  email)
        {
            HttpResponseMessage response =await _httpClient.GetAsync($"Utility/ValidateEmail?Email={email}");
           return await Utilities.GetResponseContent<bool>(response);
        }
        public async Task<string> GetLoginTokenAsync(User user)
        {
            var content = Utilities.GetRequestContent(user);
            HttpResponseMessage response = await _httpClient.PostAsync($"Authentication/LoginUser",content);
            return  await response.Content.ReadAsStringAsync();
        }
        public async Task CreateTestAsync(SimTest simTest)
        {
            var content = Utilities.GetRequestContent(simTest);
            await _httpClient.PostAsync($"Admin/CreateTestAsync", content);
        }

        public async Task<VmQuestions> GetQuetionsBySimTestId(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Admin/GetQuetionsBySimTestId?simtestId={id}");
            return await Utilities.GetResponseContent<VmQuestions>(response);
        }

        public async Task<HttpResponseMessage> AddQuestion(Questionwithchoice questionwithchoice)
        {
            var content = Utilities.GetRequestContent(questionwithchoice);
            HttpResponseMessage response = await _httpClient.PostAsync($"Admin/AddQuestion", content);
            return response;
        }

        public async Task<HttpResponseMessage> AssignTest(Assignedtest assignedtest)
        {
            var content = Utilities.GetRequestContent(assignedtest);
            HttpResponseMessage response = await _httpClient.PostAsync($"Admin/AssignTest", content);
            return response;
        }

        public async Task<List<VmCustomer>> GetCustomers()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Admin/GetCustomers");
            return await Utilities.GetResponseContent<List<VmCustomer>>(response);
        }
        
    }
}

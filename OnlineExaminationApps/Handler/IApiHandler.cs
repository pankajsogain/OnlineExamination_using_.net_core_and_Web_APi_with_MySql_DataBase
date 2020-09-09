using OnlineExaminationApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineExaminationApps.Handler
{
   public interface IApiHandler
    {
        public Task<IList<SimTest>> GetAllSimTest();

        public Task<HttpResponseMessage> DeleteSimTest(int simtestid);
        public Task<SimTest> GetSimTest(int simtestid);

        public Task<Assignedtest> CheckResult(VmQuestions vmQuestions);

        public  Task<HttpResponseMessage> UpdateSimTest(SimTest simtest);

        public Task<IList<Countries>> GetCountries();
        public  Task<IList<States>> GetStatesByCountryId(int countryId);
        public Task<IList<Cities>> GetCitiesByStateId(int stateId);
        public  Task<string> Registration(Registration registration);
        public Task<bool> ValidateEmail(string email);
        public  Task<string> GetLoginTokenAsync(User user);

        public  Task CreateTestAsync(SimTest simTest);

        public  Task<VmQuestions> GetQuetionsBySimTestId(int id);

        public  Task<HttpResponseMessage> AddQuestion(Questionwithchoice questionwithchoice);

        public  Task<HttpResponseMessage> AssignTest(Assignedtest assignedtest);

        public  Task<List<VmCustomer>> GetCustomers();
    }
}

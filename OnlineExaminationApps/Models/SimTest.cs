using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineExaminationApps.Models
{
    public class SimTest
    {
        public int SimTestNumber { get; set; }
        public string TestName { get; set; }
        public int Duration { get; set; }
        public decimal Percentage { get; set; }
        public int NoOfQuestions { get; set; }
        public string CustId { get; set; }

    }
}

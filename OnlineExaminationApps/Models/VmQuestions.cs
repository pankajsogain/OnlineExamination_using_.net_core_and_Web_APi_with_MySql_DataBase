using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineExaminationApps.Models
{
    public class VmQuestions
    {
        public int CustId { get; set; }
        public SimTest SimTest { get; set; }
        public List<Questionwithchoice> Questionwithchoices { get; set; }
        
    }
}

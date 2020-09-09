using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineExaminationApps.Models
{
    public class Session
    {
        public static int CustId;
        public static string UserToken { get; set; }
        public static string UserName { get; set; }
        public static string Role { get; set; }
        public static Message Message { get; set; }
    }
}

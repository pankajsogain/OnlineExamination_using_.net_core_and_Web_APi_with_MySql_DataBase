﻿

using Microsoft.AspNetCore.Http;

namespace OnlineExaminationApps.Models
{
    public partial class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }        
    }
}

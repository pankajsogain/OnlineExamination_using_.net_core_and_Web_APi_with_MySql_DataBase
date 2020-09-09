

namespace OnlineExaminationApps.Models
{
    public partial class Assignedtest
    {
        public int CustId { get; set; }
        public int TestId { get; set; }
        public int NoOfCorrectAnswers { get; set; }
        public int NoOfWrongAnswers { get; set; }
        public decimal Percentage { get; set; }
        public int Id { get; set; }
    }
}

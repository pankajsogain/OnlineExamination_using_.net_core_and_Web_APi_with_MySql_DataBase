
namespace OnlineExaminationApps.Models
{
    public partial class Questions
    {
        public int IdQuestions { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public int? TestId { get; set; }
    }
}

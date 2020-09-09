
namespace OnlineExaminationApps.Models
{
    public partial class Choices
    {
        public int ChoiceId { get; set; }
        public int? QuestionId { get; set; }
        public string ChoiceOne { get; set; }
        public string ChoiceTwo { get; set; }
        public string ChoiceThree { get; set; }
        public string ChoiceFour { get; set; }
        public string Answer { get; set; }
    }
}

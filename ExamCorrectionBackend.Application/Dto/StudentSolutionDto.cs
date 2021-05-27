namespace ExamCorrectionBackend.Application.Dto
{
    public class StudentSolutionDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string StudentId { get; set; }
        public string Answer { get; set; }
    }
}
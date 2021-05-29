namespace ExamCorrectionBackend.Application.Dto
{
    public class ExamTaskDto
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string Description { get; set; }
        public string Solution { get; set; }
        public int? TaskOrder { get; set; }
    }
}
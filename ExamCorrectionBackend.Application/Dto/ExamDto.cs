using System;

namespace ExamCorrectionBackend.Application.Dto
{
    public class ExamDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public decimal ScoreThreshold { get; set; }
        public string CourseName { get; set; }
    }
}
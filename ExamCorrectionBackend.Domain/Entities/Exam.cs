using System;
using System.Collections.Generic;

#nullable disable

namespace ExamCorrectionBackend.Domain.Entities
{
    public partial class Exam
    {
        public Exam()
        {
            ExamTasks = new HashSet<ExamTask>();
        }

        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public decimal ScoreThreshold { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<ExamTask> ExamTasks { get; set; }
    }
}

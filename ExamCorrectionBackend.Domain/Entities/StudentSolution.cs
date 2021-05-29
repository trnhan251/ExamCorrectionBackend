using System;
using System.Collections.Generic;

#nullable disable

namespace ExamCorrectionBackend.Domain.Entities
{
    public partial class StudentSolution
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string StudentId { get; set; }
        public string Answer { get; set; }
        public decimal? Score { get; set; }

        public virtual ExamTask Task { get; set; }
    }
}

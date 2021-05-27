using System;
using System.Collections.Generic;

#nullable disable

namespace ExamCorrectionBackend.Domain.Entities
{
    public partial class StudentSolution
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public string StudentId { get; set; }
        public string Answer { get; set; }

        public virtual ExamTask Task { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ExamCorrectionBackend.Domain.Entities
{
    public partial class ExamTask
    {
        public ExamTask()
        {
            StudentSolutions = new HashSet<StudentSolution>();
        }

        public long Id { get; set; }
        public long ExamId { get; set; }
        public string Description { get; set; }
        public string Solution { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual ICollection<StudentSolution> StudentSolutions { get; set; }
    }
}

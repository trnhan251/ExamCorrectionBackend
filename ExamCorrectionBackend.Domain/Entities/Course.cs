using System;
using System.Collections.Generic;

#nullable disable

namespace ExamCorrectionBackend.Domain.Entities
{
    public partial class Course
    {
        public Course()
        {
            Exams = new HashSet<Exam>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
    }
}

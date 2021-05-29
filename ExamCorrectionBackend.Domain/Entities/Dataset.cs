using System;
using System.Collections.Generic;

#nullable disable

namespace ExamCorrectionBackend.Domain.Entities
{
    public partial class Dataset
    {
        public int Id { get; set; }
        public string Sentence1 { get; set; }
        public string Sentence2 { get; set; }
        public decimal Score { get; set; }
        public bool IsSimilar { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}

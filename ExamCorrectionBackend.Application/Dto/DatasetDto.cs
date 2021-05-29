using System;

namespace ExamCorrectionBackend.Application.Dto
{
    public class DatasetDto
    {
        public int Id { get; set; }
        public string Sentence1 { get; set; }
        public string Sentence2 { get; set; }
        public decimal Score { get; set; }
        public bool IsSimilar { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
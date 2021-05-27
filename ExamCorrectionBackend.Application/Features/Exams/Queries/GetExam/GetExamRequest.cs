using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Exams.Queries.GetExam
{
    public class GetExamRequest : IRequest<ExamDto>
    {
        public int ExamId { get; set; }
        public string UserId { get; set; }
    }
}
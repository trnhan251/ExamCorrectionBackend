using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Exams.Commands.DeleteExam
{
    public class DeleteExamRequest : IRequest<ExamDto>
    {
        public int ExamId { get; set; }
        public string UserId { get; set; }
    }
}
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Exams.Commands.UpdateExam
{
    public class UpdateExamRequest : IRequest<ExamDto>
    {
        public ExamDto ExamDto { get; set; }
        public string UserId { get; set; }
    }
}